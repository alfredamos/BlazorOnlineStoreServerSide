using AutoMapper;
using BlazorOnlineStoreServerSide.Contracts;
using BlazorOnlineStoreServerSide.ViewModels;
using BlazorOnlineStoreServerSide.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Pages.Customers
{
    public class CustomerListBase : ComponentBase
    {
        [Inject]
        public ICustomerRepository CustomerService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public List<Customer> CustomersT { get; set; } = new List<Customer>();

        public List<CustomerView> Customers { get; set; } = new List<CustomerView>();

        public List<Address> Addresses { get; set; } = new List<Address>();

        public AddressView Address { get; set; } = new AddressView();

        public string FullName { get; set; }

        protected async override Task OnInitializedAsync()
        {
            CustomersT = (await CustomerService.GetAll()).ToList();            

            Mapper.Map(CustomersT, Customers);
        }

        protected async Task HandleSearch(string searchKey)
        {
            CustomersT = (await CustomerService.Search(searchKey)).ToList();
            Mapper.Map(CustomersT, Customers);
        }

        protected void CreateCustomer()
        {
            NavigationManager.NavigateTo("/addCustomer");
        }

        protected void UpdateCustomer(int customerId)
        {
            NavigationManager.NavigateTo($"/editCustomer/{customerId}");
        }

        protected void DeleteCustomer(int customerId)
        {
            NavigationManager.NavigateTo($"/deleteCustomer/{customerId}");
        }

        protected void DetailCustomer(int customerId)
        {
            NavigationManager.NavigateTo($"/customerDetails/{customerId}");
        }
    }
}
