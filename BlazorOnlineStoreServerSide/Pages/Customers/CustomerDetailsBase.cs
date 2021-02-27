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

namespace BlazorOnlineStoreServerSide.Pages.Customers
{
    public class CustomerDetailsBase : ComponentBase
    {
        [Inject]
        public ICustomerRepository CustomerService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public int Id { get; set; }

        public Customer CustomerT { get; set; } = new Customer();

        public CustomerView Customer { get; set; } = new CustomerView();

        public AddressView Address { get; set; } = new AddressView();

        protected ConfirmDelete DeleteConfirmation { get; set; }

        public string NameOfCustomer { get; set; }

        protected bool ShowFooter { get; set; } = false;

        protected bool HideFooter { get; set; } = true;

        protected async override Task OnInitializedAsync()
        {
            CustomerT = await CustomerService.GetById(Id);

            Mapper.Map(CustomerT, Customer);

            Address = Customer.Addresses.FirstOrDefault(da => da.IsBillingAddress == true || 
                                         da.IsShippingAddress == da.IsBillingAddress);

            NameOfCustomer = $"{Customer.FirstName} {Customer.LastName}";
        }

        protected void UpdateCustomer(int customerId)
        {           
            
            NavigationManager.NavigateTo($"/editCustomer/{customerId}");
           
        }

        protected void DeleteClick()
        {
            DeleteConfirmation.Show();
        }

        protected async Task CustomerToDelete(bool deteConfirmed)
        {
            Mapper.Map(Customer, CustomerT);

            if (deteConfirmed)
            {
                await CustomerService.Delete(Id);

            }
            NavigationManager.NavigateTo("/listOfCustomers");

        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/listOfCustomers");
        }

        protected void ShowFooterMethod()
        {
            ShowFooter = true;
            HideFooter = false;
            StateHasChanged();
        }

        protected void HideFooterMethod()
        {
            ShowFooter = false;
            HideFooter = true;
            StateHasChanged();
        }

    }
}

