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
    public class ListOfProductsBase : ComponentBase
    {
        [Inject]
        public IProductRepository ProductService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public List<ProductView> Products { get; set; } = new List<ProductView>();

        public List<Product> ProductsT { get; set; } = new List<Product>();

        protected override async Task OnInitializedAsync()
        {
            ProductsT = (await ProductService.GetAll()).ToList();

            Mapper.Map(ProductsT, Products);
        }

        protected async Task HandleSearch(string searchKey)
        {
            ProductsT = (await ProductService.Search(searchKey)).ToList();
            Mapper.Map(ProductsT, Products);
        }

        protected void CreateProduct()
        {
            NavigationManager.NavigateTo("/addProduct");
        }

        protected void AddProductToCart(int productId)
        {
            NavigationManager.NavigateTo($"/addToCart/{productId}");
        }

    }
}
