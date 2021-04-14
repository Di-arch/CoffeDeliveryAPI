using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeDeliveryDB_API.RabbitMQ
{
    public interface IDbChangesNotifier
    {
        public void SendMessage(string message);
    }
}
