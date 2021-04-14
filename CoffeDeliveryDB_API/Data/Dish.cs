using System;
using System.Collections.Generic;

#nullable disable

namespace CoffeDeliveryDB_API
{
    public partial class Dish
    {
        public Dish()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
