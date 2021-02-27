using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string UniqueID { get; set; }
        public string AdminUser { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public DateTime DatePlaced { get; set; } = DateTime.Now;
        public DateTime? DateProcessed { get; set; }
        public List<OrderLineItem> OrderLineItems { get; set; }

    }
}
