using CoffeDeliveryDB_API.Data;
using CoffeDeliveryDB_API.RabbitMQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeDeliveryDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeDeliveryController : ControllerBase
    {
        private readonly IDbService _dbService;
        private readonly IDbChangesNotifier _dbChangesNotifier;
        private readonly IConfiguration _configuration;
        public CoffeDeliveryController(IDbService dbService, IDbChangesNotifier dbChangesNotifier, IConfiguration configuration)
        {
            _dbService = dbService;
            _dbChangesNotifier = dbChangesNotifier;
            _configuration = configuration;
        }
        [HttpGet("[action]")]
        public List<Dish> GetDishes()
        {
            return _dbService.GetDishes().ToList();
        }
        [HttpGet("[action]/{id}")]
        public Dish GetDish(int id)
        {
            return _dbService.GetDish(id);
        }
        [HttpPost("[action]")]
        public int CreateDish([FromBody] Dish dish)
        {
            var result = _dbService.CreateDish(dish);
            _dbChangesNotifier.SendMessage(_configuration["RabbitMq:DishesChangeMsg"]);
            return result;
        }
        [HttpPut("[action]")]
        public ActionResult<bool> UpdateDish([FromBody] Dish dish)
        {
            try
            {
                var result = _dbService.UpdateDish(dish);
                _dbChangesNotifier.SendMessage(_configuration["RabbitMq:DishesChangeMsg"]);
                return result;
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpDelete("[action]/{id}")]
        public bool DeleteDish(int id)
        {
            var result = _dbService.DeleteDish(id);
            _dbChangesNotifier.SendMessage(_configuration["RabbitMq:DishesChangeMsg"]);
            return result;
        }
        [HttpGet("[action]")]
        public List<Order> GetOrders()
        {
            return _dbService.GetOrders().ToList();
        }
        [HttpGet("[action]/{user}")]
        public List<Order> GetOrdersByUser(string user)
        {
            return _dbService.GetOrdersByUser(user).ToList();
        }
        [HttpGet("[action]/page/rowsPerPage")]
        public List<Order> GetOrder(int page,int rowsPerPage)
        {
            return _dbService.GetOrders().ToList() ;
        }
        [HttpGet("[action]/{id}")]
        public Order GetOrder(int id)
        {
            return _dbService.GetOrder(id);
        }
        [HttpGet("[action]")]
        public List<Order> GetOrdersForCooking()
        {
            return _dbService.GetOrdersForCooking().ToList();
        }
        [HttpGet("[action]")]
        public List<Order> GetOrdersForDelivery()
        {
            return _dbService.GetOrdersForDelivery().ToList(); 
        }
        [HttpGet("[action]/{cookerId}")]
        public List<Order> GetOrderByCooker( string cookerId)
        {
            return _dbService.GetOrdersByCooker(cookerId).ToList();
        }
        [HttpGet("[action]/{courierId}")]
        public List<Order> GetOrderByCourier(string courierId)
        {
            return _dbService.GetOrdersByCourier(courierId).ToList();
        }
        [HttpPost("[action]")]
        public int CreateOrder([FromBody] Order order)
        {
            var result = _dbService.CreateOrder(order);
            _dbChangesNotifier.SendMessage(_configuration["RabbitMq:OrdersChangedMsg"]);
            return result;
        }
        [HttpPut("[action]")]
        public bool UpdateOrder([FromBody]  Order order)
        {
            var result = _dbService.UpdateOrder(order);
            _dbChangesNotifier.SendMessage(_configuration["RabbitMq:OrdersChangedMsg"]);
            return result;
        }
        [HttpDelete("[action]/{id}")]
        public bool DeleteOrder(int id)
        {
            var result= _dbService.DeleteOrder(id);
            _dbChangesNotifier.SendMessage(_configuration["RabbitMq:OrdersChangedMsg"]);
            return result;
        }
    }
}
