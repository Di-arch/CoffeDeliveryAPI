using System;
using System.Collections.Generic;

#nullable disable

namespace CoffeDeliveryDB_API
{
    public partial class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? DishId { get; set; }
        public int? Quantity { get; set; }
        public string Address { get; set; }
        public string CookerId { get; set; }
        public string CourierId { get; set; }
        public DateTime? Datetimeorderplaced { get; set; }
        public DateTime? Datetimeordercooked { get; set; }
        public DateTime? Datetimeorderdelivered { get; set; }
        public virtual Dish Dish { get; set; }
    }
}
