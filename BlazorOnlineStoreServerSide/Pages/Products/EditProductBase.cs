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
    public class EditProductBase : ComponentBase
    {
        [Inject]
        public IFileStorageService FileStorageService { get; set; }

        [Inject]
        public IProductRepository ProductService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public int Id { get; set; }

        public Product ProductT { get; set; } = new Product();

        public ProductView Product { get; set; } = new ProductView();

        protected override async Task OnInitializedAsync()
        {
            ProductT = await ProductService.GetById(Id);

            Mapper.Map(ProductT, Product);
        }

        protected async Task UpdateProduct()
        {
            Mapper.Map(Product, ProductT);

            if (!string.IsNullOrWhiteSpace(ProductT.ImageLink))
            {
                var productImageLink = Convert.FromBase64String(ProductT.ImageLink);
                ProductT.ImageLink = await FileStorageService.EditFile(productImageLink, "jpg", "product", ProductT.ImageLink);
            }

            var product = await ProductService.Update(ProductT);

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
