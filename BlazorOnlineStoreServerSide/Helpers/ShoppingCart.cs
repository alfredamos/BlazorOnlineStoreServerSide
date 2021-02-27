using BlazorOnlineStoreServerSide.Contracts;
using BlazorOnlineStoreServerSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Helpers
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly IOrderRepository _orderService;       
        private readonly IPlaceOrder _placeOrder;
        private List<OrderLineItem> OrderLineItems = new List<OrderLineItem>();
       
        public event Action OnChange;

        public ShoppingCart(IOrderRepository orderService, IPlaceOrder placeOrder)
        {
            _orderService = orderService;            
            _placeOrder = placeOrder;
        }
        
        public List<OrderLineItem> AddProductToOrder(Product product, int quantity)
        {            
            OrderLineItems.Add(new OrderLineItem { ProductID = product.ProductID, Price = product.Price, Quantity = quantity });

            OrderChange();

            return OrderLineItems;
        }

        public void DeleteProductFromOrder(int productId)
        {
            var item = OrderLineItems.FirstOrDefault(x => x.ProductID == productId);
            if (item != null) OrderLineItems.Remove(item);
            OrderChange();
        }

        public void EmptyOrder ()
        {
            OrderLineItems.Clear();
            OnChange();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _orderService.GetById(id);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _orderService.GetAll();
        }

        public async Task<string> PlaceOrder(Order order, Customer customer)
        {           
            order.OrderLineItems = OrderLineItems;

            var result = await _placeOrder.CreateOrder(order, customer);

            EmptyOrder();
                              
            OrderChange();

            return result.UniqueID;
        }
      
        public async Task<Order> UpdateOrder(Order order, Customer customer)
        {      
            var result = await _placeOrder.UpdateOrder(order, customer );

            OrderChange();

            return result;
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var item = OrderLineItems.FirstOrDefault(x => x.ProductID == productId);
            var itemNew = item;           
            itemNew.Quantity = quantity;
            OrderLineItems[OrderLineItems.FindIndex(ind => ind.Equals(item))] = itemNew;            
            OrderChange();

        }

        void OrderChange() => OnChange.Invoke();

        public List<OrderLineItem> GetAllOrderLineItems()
        {
            
            return OrderLineItems;
        }

        public List<OrderLineItem> GetAllOrderLineItems(List<OrderLineItem> orderLineItems)
        {
            OrderLineItems = orderLineItems;
            OrderChange();

            return OrderLineItems;
        }
    }
}
