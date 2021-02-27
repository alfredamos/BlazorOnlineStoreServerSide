using BlazorOnlineStoreServerSide.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.ViewModels
{
    public class OrderView
    {
        public int OrderID { get; set; }
        public string UniqueID { get; set; }
        public string AdminUser { get; set; }
        public int CustomerID { get; set; }
        public CustomerView Customer { get; set; }
        public DateTime DatePlaced { get; set; } = DateTime.Now;
        public DateTime? DateProcessed { get; set; }
        public List<OrderLineItem> OrderLineItems { get; set; }

    }
}
