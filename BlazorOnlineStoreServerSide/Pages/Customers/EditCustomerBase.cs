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

namespace BlazorOnlineStoreServerSide.Pages.Customers
{
    public class EditCustomerBase : ComponentBase
    {
        [Inject]
        public IFileStorageService FileStorageService { get; set; }

        [Inject]
        public IAddCardAndAddressToCustomerInfo AddCardAndAddressToCustomerInfo { get; set; }

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

        public List<Address> AddressesT { get; set; } = new List<Address>();

        public List<AddressView> Addresses { get; set; } = new List<AddressView>();

        public AddressView Address { get; set; } = new AddressView();

        public CardDetailView CardDetail { get; set; } = new CardDetailView();

        public CardDetail CardDetailT { get; set; } = new CardDetail();

        public List<CardDetailView> CardDetails { get; set; } = new List<CardDetailView>();

        public List<CardDetail> CardDetailsT { get; set; } = new List<CardDetail>();

        public BillingAddress CardAddress { get; set; } = new BillingAddress();

        public bool ShowPaymentForm { get; set; } = false;

        public bool ShowCustomerForm { get; set; } = true;

        protected async override Task OnInitializedAsync()
        {
            CustomerT = await CustomerService.GetById(Id);

            CardDetailsT = CustomerT.CardDetails;
            Mapper.Map(CardDetailsT, CardDetails);

            Mapper.Map(CustomerT, Customer);
            Addresses = Customer.Addresses;

            Address = Addresses.FirstOrDefault(da => da.IsBillingAddress == true ||
                                         da.IsShippingAddress == da.IsBillingAddress);

            CardDetail = CardDetails.FirstOrDefault(xx => xx.CustomerCardID == Id);

            Mapper.Map(Address, CardAddress);

            AddCardAndAddressToCustomerInfo.OnChange += StateHasChanged;
        }

        protected async Task UpdateCustomer()
        {
            Addresses = AddCardAndAddressToCustomerInfo.AddAddressToCustomerInfo(Address);

            Mapper.Map(Customer, CustomerT);
            Mapper.Map(Addresses, AddressesT);

            AddCardAndAddressToCustomerInfo.ClearAddresses = true;
            AddCardAndAddressToCustomerInfo.ClearList();
            AddCardAndAddressToCustomerInfo.ClearAddresses = false;

            if (ShowPaymentForm)
            {
                Mapper.Map(Address, CardAddress);
                CardDetails = AddCardAndAddressToCustomerInfo.AddCardDetailsToCustomerInfo(CardDetail, CardAddress);

                Mapper.Map(CardDetails, CardDetailsT);

                CustomerT.CardDetails = CardDetailsT;

                AddCardAndAddressToCustomerInfo.ClearCardDetails = true;
                AddCardAndAddressToCustomerInfo.ClearList();
                AddCardAndAddressToCustomerInfo.ClearCardDetails = false;
            }

            CustomerT.Addresses = AddressesT;

            if (!string.IsNullOrWhiteSpace(CustomerT.CustomerPhoto))
            {
                var customerPhoto = Convert.FromBase64String(CustomerT.CustomerPhoto);
                CustomerT.CustomerPhoto = await FileStorageService.EditFile(customerPhoto, "jpg", "customer", CustomerT.CustomerPhoto);
            }

            var product = await CustomerService.Update(CustomerT);

            if (product != null)
            {
                NavigationManager.NavigateTo("/listOfCustomers");
            }
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/listOfCustomers");
        }

        protected void EditPaymentInfo()
        {
            ShowPaymentForm = true;
            ShowCustomerForm = false;
        }

        protected void CancelPay()
        {
            ShowPaymentForm = false;
            ShowCustomerForm = true;
            NavigationManager.NavigateTo($"/editCustomer/{Id}");
        }
    }
}
