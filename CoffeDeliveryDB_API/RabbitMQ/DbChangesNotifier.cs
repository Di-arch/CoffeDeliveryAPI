using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeDeliveryDB_API.RabbitMQ
{
    public class DbChangesNotifier:IDbChangesNotifier
    {
        private readonly IConfiguration _config;
        public DbChangesNotifier(IConfiguration configuration)
        {
            _config = configuration;
        }
        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(_config.GetConnectionString("MqCloudConnectionString")) };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var body = Encoding.UTF8.GetBytes(message);

                channel.QueueDeclare(queue: _config["RabbitMq:DbChangedQueueName"],
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.BasicPublish(exchange: "",
                                     routingKey: _config["RabbitMq:DbChangedQueueName"],
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
