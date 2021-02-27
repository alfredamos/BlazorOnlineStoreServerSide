using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Models
{
    public class BillingAddress
    {
        public int BillingAddressID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public bool IsHomeAddress { get; set; } = false;
        public bool IsBillingAddress { get; set; } = false;
        public bool IsShippingAddress { get; set; } = false;

        public int CardAddressID { get; set; }
        public CardDetail Card { get; set; }
    }
}
