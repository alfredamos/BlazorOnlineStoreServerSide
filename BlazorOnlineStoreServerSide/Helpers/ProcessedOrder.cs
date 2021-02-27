using AutoMapper;
using BlazorOnlineStoreServerSide.Contracts;
using BlazorOnlineStoreServerSide.ViewModels;
using BlazorOnlineStoreServerSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Helpers
{
    public class ProcessedOrder : IProcessedOrder
    {
        private readonly IOrderRepository _orderService;
        private readonly IMapper _mapper;

        public event Action OnChange;
        public Order Order { get; set; } = new Order();

        public ProcessedOrder(IOrderRepository orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<Order> UpdateOrder(OrderView orderView)
        {           
            _mapper.Map(orderView, Order);
            Order.DateProcessed = DateTime.Now;
            var order = await _orderService.Update(Order);
            NotifyDataChanged();

            return order;
        }

        private void NotifyDataChanged() => OnChange.Invoke();
    }
}
