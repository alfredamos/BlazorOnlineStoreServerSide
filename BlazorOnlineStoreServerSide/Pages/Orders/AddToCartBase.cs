using AutoMapper;
using BlazorOnlineStoreServerSide.Contracts;
using BlazorOnlineStoreServerSide.ViewModels;
using BlazorOnlineStoreServerSide.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Pages.Orders
{
    public class AddToCartBase : ComponentBase
    {
        [Inject]
        public ICardDetailRepository CardDetailService { get; set; }

        [Inject]
        public ICustomerRepository CustomerService { get; set; }

        [Inject]
        public IOrderRepository OrderService { get; set; }

        [Inject]
        public IProductRepository ProductService { get; set; }

        [Inject]
        public IShoppingCart ShoppingCart { get; set; }

        [Inject]
        public IAddCardAndAddressToCustomerInfo AddCardAndAddressToCustomerInfo { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Parameter]
        public int ProductId { get; set; }

        public int Quantity { get; set; } = 1;

        protected bool ShowOrder = true;

        protected bool ShowCustomerInfo = false;

        public List<OrderLineItem> OrderLineItems { get; set; } = new List<OrderLineItem>();

        public List<Product> ProductsT { get; set; } = new List<Product>();

        public List<ProductView> Products { get; set; } = new List<ProductView>();

        public Product Product { get; set; } = new Product();

        public Order OrderT { get; set; } = new Order();

        public OrderView Order { get; set; } = new OrderView();

        public Customer CustomerT { get; set; } = new Customer();

        public CustomerView Customer { get; set; } = new CustomerView();

        public AddressView Address { get; set; } = new AddressView();

        public BillingAddress CardAddress { get; set; } = new BillingAddress();

        public List<AddressView> Addresses { get; set; } = new List<AddressView>();

        public CardDetailView CardDetail { get; set; } = new CardDetailView();

        public CardDetail CardDetailT { get; set; } = new CardDetail();

        public List<CardDetailView> CardDetails { get; set; } = new List<CardDetailView>();

        public List<CardDetail> CardDetailsT { get; set; } = new List<CardDetail>();

        public List<Customer> CustomersT { get; set; } = new List<Customer>();

        public List<CustomerView> Customers { get; set; } = new List<CustomerView>();

        public string UserName { get; set; }

        //private AuthenticationState authState;

        protected override async Task OnInitializedAsync()
        {            
            if (Customer == null) InstantiateNullCustomer(); //----> Initialize the customer with a victitious email to prevent the program from breaking when it is null.
            
            ProductsT = (await ProductService.GetAll()).ToList();
            Mapper.Map(ProductsT, Products);

            Product = ProductsT.FirstOrDefault(pd => pd.ProductID == ProductId);

            ShoppingCart.OnChange += StateHasChanged;
            AddCardAndAddressToCustomerInfo.OnChange += StateHasChanged;

            OrderLineItems = ShoppingCart.AddProductToOrder(Product, Quantity);
        }

        protected void UpdateQuantity(OrderLineItem orderLineItem)
        {
            var productId = orderLineItem.ProductID;
            var quantity = orderLineItem.Quantity;
            ShoppingCart.UpdateQuantity(productId, quantity);
        }

        protected void RemoveProduct(OrderLineItem orderLineItem)
        {
            var productId = orderLineItem.ProductID;
            ShoppingCart.DeleteProductFromOrder(productId);
        }


        protected void PlaceOrder()
        {                                      
            ShowOrder = false;
            ShowCustomerInfo = true;                 
           
        }

        protected async Task CreateOrder()
        {
            if (Customer.SameAddress) Address.IsBillingAddress = Address.IsShippingAddress = Customer.SameAddress; //----> Shipping and Billing addresses are same.
            else Address.IsBillingAddress = true; //----> Billing BillingAddress is used for billing the card.

            CustomersT = (await CustomerService.GetAll()).ToList();
            Mapper.Map(CustomersT, Customers);
            Mapper.Map(Address, CardAddress);

            var customer = Customers.FirstOrDefault(xx => xx.Email == Customer.Email);

            if (customer == null)
            {                
                Addresses = AddCardAndAddressToCustomerInfo.AddAddressToCustomerInfo(Address);
                Customer.Addresses = Addresses;

                CardDetails = AddCardAndAddressToCustomerInfo.AddCardDetailsToCustomerInfo(CardDetail, CardAddress);
                Customer.CardDetails = CardDetails;

                Mapper.Map(Customer, CustomerT);
            }
            else
                Mapper.Map(customer, CustomerT);
                                             
            var customert = new Customer();
            
            if (customer == null)            
                customert = await CustomerService.Add(CustomerT);           
            else if (customer != null)            
                customert = CustomerT;
       
            PlacedOrderHelpMethod(customert);

        }

        protected void Cancel()
        {
            ShowOrder = true;
            ShowCustomerInfo = false;
            NavigationManager.NavigateTo($"/addToCart/{ProductId}");
        }

        protected void ContinueShopping()
        {
            NavigationManager.NavigateTo("/listOfProducts");
        }

        protected void Back()
        {
            NavigationManager.NavigateTo("/listOfProducts");
        }

        private async void PlacedOrderHelpMethod(Customer customer)
        {           
            OrderT.CustomerID = customer.CustomerID;            
            OrderT.AdminUser = "admin";

            var result = await ShoppingCart.PlaceOrder(OrderT, customer);

            if (result != null)
            {
                NavigationManager.NavigateTo($"/placeOrder/{result}");
            }
        }
       
        private void InstantiateNullCustomer()
        {            
            Customer = new CustomerView { Email = "joedoe@gmail.com", Phone = "2378345617" }; //----> Initialize the customer with a victitious email to prevent the program from breaking when it is null.            
        }
        
    }
}
