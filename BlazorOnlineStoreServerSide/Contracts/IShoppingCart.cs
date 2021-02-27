using BlazorOnlineStoreServerSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Contracts
{
    public interface IShoppingCart
    {
        event Action OnChange;
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrder(int id);
        List<OrderLineItem> AddProductToOrder(Product product, int quantity);
        void UpdateQuantity(int productId, int quantity);
        Task<Order> UpdateOrder(Order order, Customer customer);
        void DeleteProductFromOrder(int productId);
        Task<string> PlaceOrder(Order order, Customer customer);
        void EmptyOrder();
        List<OrderLineItem> GetAllOrderLineItems();        
        List<OrderLineItem> GetAllOrderLineItems(List<OrderLineItem> orderLineItems);
    }
}
