using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.ViewModels
{
    public class CustomerView
    {
        public int CustomerID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string CustomerPhoto { get; set; }
        public bool SameAddress { get; set; } = false;
        public bool SaveInfo { get; set; } = false;
        public List<AddressView> Addresses { get; set; }
        public List<CardDetailView> CardDetails { get; set; }

    }
}
