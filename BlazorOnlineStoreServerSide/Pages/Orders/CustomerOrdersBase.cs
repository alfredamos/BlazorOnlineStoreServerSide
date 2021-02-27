using AutoMapper;
using BlazorOnlineStoreServerSide.Contracts;
using BlazorOnlineStoreServerSide.ViewModels;
using BlazorOnlineStoreServerSide.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Pages.Orders
{
    public class CustomerOrdersBase : ComponentBase
    {
        [Inject]
        public IOrderRepository OrderService { get; set; }

        [Inject]
        public IProductRepository ProductService { get; set; }

        [Inject]
        public IProcessedOrder ProcessedOrder { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public List<OrderView> Orders { get; set; } = new List<OrderView>();

        public List<Order> OrdersT { get; set; } = new List<Order>();

        public List<ProductView> Products { get; set; } = new List<ProductView>();

        public List<Product> ProductsT { get; set; } = new List<Product>();

        [Parameter]
        public int CustomerId { get; set; }

        public double Total { get; set; } = 0.0;

        public double SubTotal { get; set; } = 0.0;

        protected override async Task OnInitializedAsync()
        {
            ProductsT = (await ProductService.GetAll()).ToList();
            OrdersT = (await OrderService.GetAll()).Where(od => od.CustomerID == CustomerId &&
                       !od.DateProcessed.HasValue)
                      .OrderByDescending(od => od.DatePlaced).ToList();

            Mapper.Map(OrdersT, Orders);
            Mapper.Map(ProductsT, Products);

        }

        public async void OrderByDateProcessed(OrderView order)
        {
            var processedOrder = await ProcessedOrder.UpdateOrder(order);

            if (processedOrder != null)
            {
                NavigationManager.NavigateTo("/orderProcessed");
            }
        }

        protected async void HandleSearch(string searchKey)
        {
            OrdersT = (await OrderService.Search(searchKey)).Where(od => od.CustomerID == CustomerId &&
                       !od.DateProcessed.HasValue)
                      .OrderByDescending(od => od.DatePlaced).ToList();

            Mapper.Map(OrdersT, Orders);
        }

    }
}
