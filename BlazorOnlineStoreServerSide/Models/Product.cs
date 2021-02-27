using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageLink { get; set; }
        public string Description { get; set; }
    }
}
