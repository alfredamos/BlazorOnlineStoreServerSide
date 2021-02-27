using AutoMapper;
using BlazorOnlineStoreServerSide.Contracts;
using BlazorOnlineStoreServerSide.ViewModels;
using BlazorOnlineStoreServerSide.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorOnlineStoreServerSide.PhotoHelper;

namespace BlazorOnlineStoreServerSide.Pages.Products
{
    public class AddProductBase : ComponentBase
    {
        [Inject]
        public IFileStorageService FileStorageService { get; set; }

        [Inject]
        public IProductRepository ProductService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public Product ProductT { get; set; } = new Product();

        public ProductView Product { get; set; } = new ProductView();

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        protected async Task CreateProduct()
        {
            Mapper.Map(Product, ProductT);

            if (!string.IsNullOrWhiteSpace(ProductT.ImageLink))
            {
                var productImageLink = Convert.FromBase64String(ProductT.ImageLink);
                ProductT.ImageLink = await FileStorageService.SaveFile(productImageLink, "jpg", "product");
            }

            var product = await ProductService.Add(ProductT);

            if (product != null)
            {
                NavigationManager.NavigateTo("/productList");
            }
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/productList");
        }
    }
}
