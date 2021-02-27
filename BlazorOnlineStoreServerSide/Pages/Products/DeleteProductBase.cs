using AutoMapper;
using BlazorOnlineStoreServerSide.Contracts;
using BlazorOnlineStoreServerSide.Shared;
using BlazorOnlineStoreServerSide.ViewModels;
using BlazorOnlineStoreServerSide.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Pages.Products
{
    public class DeleteProductBase : ComponentBase
    {
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

        protected ConfirmDelete DeleteConfirmation { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ProductT = await ProductService.GetById(Id);

            Mapper.Map(ProductT, Product);
          
        }

        protected void DeleteClick()
        {
            DeleteConfirmation.Show();
        }

        protected async Task DeleteProduct(bool deleteConfirmed)
        {
            Mapper.Map(Product, ProductT);

            if (deleteConfirmed) await ProductService.Delete(Id);

            NavigationManager.NavigateTo("/productList");
           
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/productList");
        }
    }
}
