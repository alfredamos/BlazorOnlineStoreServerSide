using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Models
{
    public class Customer       
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }       
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CustomerPhoto { get; set; }
        public bool SameAddress { get; set; } = false;
        public bool SaveInfo { get; set; } = false;
        public List<Address> Addresses { get; set; }
        public List<CardDetail> CardDetails { get; set; }
    }
}
