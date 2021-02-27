using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Models
{
    public class CardDetail
    {
        public int CardDetailID { get; set; }        
        public string NameOnCard { get; set; }       
        public string CardNumber { get; set; }       
        public string CardSecurityNumber { get; set; }
        public BillingAddress BillingAddress { get; set; }       
        public DateTime ExpiryDate { get; set; }

        public int CustomerCardID { get; set; }
        public Customer Customer { get; set; }
    }
}
