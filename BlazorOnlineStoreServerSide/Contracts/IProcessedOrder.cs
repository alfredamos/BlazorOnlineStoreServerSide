using BlazorOnlineStoreServerSide.ViewModels;
using BlazorOnlineStoreServerSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Contracts
{
    public interface IProcessedOrder
    {
        event Action OnChange;
        Task<Order> UpdateOrder(OrderView orderView);
    }
}
