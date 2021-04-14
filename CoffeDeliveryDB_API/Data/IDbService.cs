using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeDeliveryDB_API.Data
{
    public interface IDbService
    {
        public IEnumerable<Dish> GetDishes();
        public Dish GetDish(int id);
        public int CreateDish(Dish dish);
        public bool UpdateDish(Dish dish);
        public bool DeleteDish(int dishId);
        public IEnumerable<Order> GetOrders();
        public IEnumerable<Order> GetOrders(int page,int rowPerPage);
        public Order GetOrder(int id);
        public IEnumerable<Order> GetOrdersByUser(string user);
        public IEnumerable<Order> GetOrdersForCooking();
        public IEnumerable<Order> GetOutDatedOrdersForCooking();
        public IEnumerable<Order> GetOrdersForDelivery();
        public IEnumerable<Order> GetOutdatedOrdersForDelivery();
        public IEnumerable<Order> GetOrdersByCooker(string cookerId);
        public IEnumerable<Order> GetOrdersByCourier(string courierId);
        public int CreateOrder(Order order);
        public bool UpdateOrder(Order order);
        public bool DeleteOrder(int orderID);
    }
}
