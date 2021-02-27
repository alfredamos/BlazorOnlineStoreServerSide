using BlazorOnlineStoreServerSide.Contracts;
using BlazorOnlineStoreServerSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Helpers
{
    public class OrderCertifyer : IOrderCertifyer
    {
        public bool ValidateCustomerInformation(
            string name,
            string address,
            string city,
            string state,
            string country)
        {           
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(state) ||
                string.IsNullOrWhiteSpace(country)) return false;

            return true;
        }

        public bool ValidateCreateOrder(Order order, Customer customer)
        {        
            var address = customer.Addresses.FirstOrDefault(da => da.IsShippingAddress == da.IsBillingAddress ||
                          da.IsBillingAddress == true);
            
            var fullName = $"{customer.FirstName} {customer.LastName}";

            //----> Order has to exist.
            if (order == null) return false;
            
            //----> Order has to have line items.
            if (order.OrderLineItems == null || order.OrderLineItems.Count <= 0) return false;            

            //----> Validating line items.
            foreach (var item in order.OrderLineItems)
            {
                if (item.ProductID <= 0 || item.Price <= 0 || item.Quantity <= 0) return false;
            }
           
            //----> Validate customer info.
            if (!ValidateCustomerInformation(fullName,
                address.Street, address.City,
                address.State, address.Country)) return false;            

            return true;
        }

        public bool ValidateUpdateOrder(Order order, Customer customer)
        {
            var address = customer.Addresses.FirstOrDefault(da => da.IsBillingAddress == true);
            var fullName = $"{customer.FirstName} {customer.LastName}";
            //----> Order has to exist.
            if (order == null) return false;

            if (order.OrderID.Equals(0)) return false;

            //----> Order has to have order line items.
            if (order.OrderLineItems == null || order.OrderLineItems.Count <= 0) return false;

            //----> Other dates.
            if (order.DateProcessed.HasValue) return false;

            //----> Validate UniqueID.
            if (string.IsNullOrWhiteSpace(order.UniqueID)) return false;

            //----> Validating line items.
            foreach (var item in order.OrderLineItems)
            {             
                if (item.ProductID <= 0 || item.Price <= 0 || item.Quantity <= 0 || 
                    item.OrderID <= 0) return false;
            }

            //----> Validate customer info.
            if (!ValidateCustomerInformation(fullName, address.Street,
                address.City, address.State, address.Country)) return false;
 
            return true;
        }

        public bool ValidateProcessOrder(Order order)
        {
            if (!order.DateProcessed.HasValue ||
                string.IsNullOrWhiteSpace(order.AdminUser)) return false;

            return true;
        }
    }
}
