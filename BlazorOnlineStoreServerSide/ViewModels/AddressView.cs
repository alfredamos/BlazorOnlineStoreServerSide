using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.ViewModels
{
    public class AddressView
    {
        public int AddressID { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }        
        [Required, DataType(DataType.PostalCode)]
        public string PostCode { get; set; }
        public bool IsHomeAddress { get; set; } = false;
        public bool IsBillingAddress { get; set; } = false;
        public bool IsShippingAddress { get; set; } = false;
        
        public int CustomerAddressID { get; set; }
        public CustomerView Customer { get; set; }

    }
}
