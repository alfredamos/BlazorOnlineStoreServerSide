using AutoMapper;
using BlazorOnlineStoreServerSide.Contracts;
using BlazorOnlineStoreServerSide.ViewModels;
using BlazorOnlineStoreServerSide.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Pages.Products
{
    public class ProductDetailsBase : ComponentBase
    {
        [Inject]
        public IProductRepository ProductService { get; set; }

        [Inject]
        public IShoppingCart ShoppingCart { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public int Id { get; set; }

        public List<OrderLineItem> OrderLineItems { get; set; } = new List<OrderLineItem>();

        public ProductView Product { get; set; } = new ProductView();

        public Product ProductT { get; set; } = new Product();

        protected bool IsShowProduct = true;

        protected override async Task OnInitializedAsync()
        {
            ProductT = await ProductService.GetById(Id);

            Mapper.Map(ProductT, Product);

        }
      
        protected void AddProductToCart(int productId)
        {
            NavigationManager.NavigateTo($"/addToCart/{productId}");
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/listOfProducts");
        }

    }
}
