using BlazorOnlineStoreServerSide.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.ViewModels
{
    public class CardDetailView
    {
        public int CardDetailID { get; set; }
        [Required]
        public string NameOnCard { get; set; }                
        [Required, DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }
        [Required, MaxLength(3)]
        public string CardSecurityNumber { get; set; }
        
        public BillingAddress BillingAddress { get; set; }
        [Required]
        public DateTime? ExpiryDate { get; set; }

        public int CustomerCardID { get; set; }
        public CustomerView Customer { get; set; }
    }
}
