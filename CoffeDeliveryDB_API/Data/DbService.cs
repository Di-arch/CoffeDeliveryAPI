using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeDeliveryDB_API.Data
{
    public class DbService : IDbService
    {
        private readonly CoffeDeliveryDbContext _coffeDeliveryDbContext;
        private readonly IConfiguration _configuration;
        private readonly double cookingTime;
        private readonly double deliveryTime;
        public DbService(CoffeDeliveryDbContext context,IConfiguration configuration)
        {
            _coffeDeliveryDbContext = context;
            _configuration = configuration;

            cookingTime = double.Parse(_configuration["TimeFrames:TimeForCooking"]);
            deliveryTime = double.Parse(_configuration["TimeFrames:TimeForDelivery"]);
        }
        #region-------------------------Dishes------------------------------------
        public IEnumerable<Dish> GetDishes() => _coffeDeliveryDbContext.Dishes;
         
        public Dish GetDish(int id) => _coffeDeliveryDbContext.Dishes.FirstOrDefault(d => d.Id == id) ??new Dish();
        public int CreateDish(Dish dish)
        {
            _coffeDeliveryDbContext.Dishes.Add(dish);
            _coffeDeliveryDbContext.SaveChanges();
            return dish.Id;
        }
        public bool UpdateDish(Dish dish)
        {
            var isExists = _coffeDeliveryDbContext.Dishes.Any(d => d.Id == dish.Id);
            if (isExists)
            {
                _coffeDeliveryDbContext.Dishes.Update(dish);
                _coffeDeliveryDbContext.SaveChanges();
                return true;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
        public bool DeleteDish(int dishId)
        {
            var dishToRemove = _coffeDeliveryDbContext.Dishes.FirstOrDefault(d => d.Id == dishId);
            if (dishToRemove is not null)
            {
                _coffeDeliveryDbContext.Remove(dishToRemove);
                _coffeDeliveryDbContext.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion-------------------------Dishes------------------------------------
        #region-------------------------Orders------------------------------------
        public IEnumerable<Order> GetOrders() => _coffeDeliveryDbContext.Orders;

        public IEnumerable<Order> GetOrders(int page,int rowsPerPage) => _coffeDeliveryDbContext.Orders.Include(o=>o.Dish).Skip(rowsPerPage*(page-1)).Take(rowsPerPage);

        public Order GetOrder(int id) => _coffeDeliveryDbContext.Orders.Include(o => o.Dish).FirstOrDefault(o => o.Id == id) ?? new Order();
        public IEnumerable<Order> GetOrdersByUser(string user)
        {
            var ordersByUser = _coffeDeliveryDbContext.Orders.Include(o => o.Dish).
                Where(o => o.UserId.Equals(user));
            return ordersByUser;
        }
        public IEnumerable<Order> GetOrdersForCooking()
        {
            var validTime = DateTime.Now.AddMinutes(-1 * cookingTime);

            var orderForCooking = 
                _coffeDeliveryDbContext.Orders.Include(o=>o.Dish).
                Where(o =>(o.Datetimeordercooked==null||o.Datetimeordercooked == default(DateTime))
                && validTime < o.Datetimeorderplaced
                &&  (string.IsNullOrEmpty(o.CourierId)));
            return orderForCooking ?? Enumerable.Empty<Order>();
        }
        public IEnumerable<Order> GetOutDatedOrdersForCooking()
        {
            var validTime = DateTime.Now.AddMinutes(-1 * cookingTime);

            var outdateOrderForCooking =
                _coffeDeliveryDbContext.Orders.
                Where(o => (o.Datetimeordercooked == null || o.Datetimeordercooked == default(DateTime))
                && validTime >= o.Datetimeorderplaced
                && string.IsNullOrEmpty(o.CookerId));

            return outdateOrderForCooking ?? Enumerable.Empty<Order>();
        }

        public IEnumerable<Order> GetOrdersForDelivery()
        {
            var validTime = DateTime.Now.AddMinutes(-1 *(cookingTime+ deliveryTime));

            var orderForDelvery = 
                _coffeDeliveryDbContext.Orders.
                Where(o => (o.Datetimeordercooked!=null&& o.Datetimeordercooked > default(DateTime))
                && (o.Datetimeorderdelivered == null || o.Datetimeorderdelivered == default(DateTime))
                && validTime < o.Datetimeorderplaced
                && !string.IsNullOrEmpty(o.CookerId)
                && string.IsNullOrEmpty(o.CourierId));
                                
            return orderForDelvery ?? Enumerable.Empty<Order>();
        }
        public IEnumerable<Order> GetOutdatedOrdersForDelivery()
        {
            var validTime = DateTime.Now.AddMinutes(-1 * (cookingTime + deliveryTime));

            var orderForDelvery =
                _coffeDeliveryDbContext.Orders.
                Where(o => (o.Datetimeordercooked != null && o.Datetimeordercooked > default(DateTime))
                && (o.Datetimeorderdelivered == null || o.Datetimeorderdelivered == default(DateTime))
                && validTime >= o.Datetimeorderplaced
                && !string.IsNullOrEmpty(o.CookerId)
                && string.IsNullOrEmpty(o.CourierId));

            return orderForDelvery ?? Enumerable.Empty<Order>();
        }
        public IEnumerable<Order> GetOrdersByCooker(string cookerId)
        {
            return _coffeDeliveryDbContext.Orders.Where(o => o.CookerId.Equals(cookerId)) ?? Enumerable.Empty<Order>();
        }
        public IEnumerable<Order> GetOrdersByCourier(string courierId)
        {
            return _coffeDeliveryDbContext.Orders.Where(o => o.CourierId.Equals(courierId)) ?? Enumerable.Empty<Order>();
        }
        public int CreateOrder(Order order)
        {
            _coffeDeliveryDbContext.Add(order);
            _coffeDeliveryDbContext.SaveChanges();
            return order.Id;
        }
        public bool UpdateOrder(Order order)
        {
            var isExist = _coffeDeliveryDbContext.Orders.Any(o => o.Id == order.Id);
            if (isExist)
            {
                _coffeDeliveryDbContext.Orders.Update(order);
                _coffeDeliveryDbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteOrder(int orderID)
        {
            var orderToRemove = _coffeDeliveryDbContext.Orders.FirstOrDefault(d => d.Id == orderID);
            if (orderToRemove is not null)
            {
                _coffeDeliveryDbContext.Orders.Remove(orderToRemove);
                _coffeDeliveryDbContext.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion-------------------------Orders------------------------------------
    }
}
