using BlazorOnlineStoreServerSide.Contracts;
using BlazorOnlineStoreServerSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Helpers
{
    public class PlaceOrder : IPlaceOrder
    {
        private readonly IOrderCertifyer _orderCertifyer;
        private readonly IOrderRepository _orderService;

        public PlaceOrder(IOrderCertifyer orderCertifyer, IOrderRepository orderService)
        {
            _orderCertifyer = orderCertifyer;
            _orderService = orderService;
        }

        public async Task<Order> CreateOrder(Order order, Customer customer)
        {       
            if (_orderCertifyer.ValidateCreateOrder(order, customer))
            {
                order.DatePlaced = DateTime.Now;
                order.UniqueID = Guid.NewGuid().ToString();
                var result = await _orderService.Add(order);
                return result;
            }
            return null;
        }

        public async Task<Order> UpdateOrder(Order order, Customer customer)
        {          
            if (_orderCertifyer.ValidateUpdateOrder(order, customer))
            {               
                var result = await _orderService.Update(order);
                return result;
            }
            return null;
        }

    }
}
