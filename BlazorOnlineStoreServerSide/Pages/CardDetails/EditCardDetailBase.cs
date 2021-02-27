using AutoMapper;
using BlazorOnlineStoreServerSide.Contracts;
using BlazorOnlineStoreServerSide.ViewModels;
using BlazorOnlineStoreServerSide.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Pages.CardDetails
{
    public class EditCardDetailBase : ComponentBase
    {
        [Inject]
        public ICardDetailRepository CardDetailService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public int Id { get; set; }

        public BillingAddress CardAddress { get; set; } = new BillingAddress();

        public CardDetail CardDetailT { get; set; } = new CardDetail();

        public CardDetailView CardDetail { get; set; } = new CardDetailView();

        protected async override Task OnInitializedAsync()
        {
            CardDetailT = await CardDetailService.GetById(Id);

            CardAddress = CardDetailT.BillingAddress;

            Mapper.Map(CardDetailT, CardDetail);
           
        }

        protected async Task UpdateCardDetail()
        {
            Mapper.Map(CardDetail, CardDetailT);
           
            CardDetailT.BillingAddress = CardAddress;
            
            var result = await CardDetailService.Update(CardDetailT);

            if (result != null)
            {
                NavigationManager.NavigateTo("/cardDetailList");
            }

        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/cardDetailList");
        }

        private void PrintCardAddress(BillingAddress address)
        {
            Console.WriteLine("This Billing Address");
            Console.WriteLine("Street : " + address.Street);
            Console.WriteLine("City : " + address.City);
            Console.WriteLine("State : " + address.State);
            Console.WriteLine("Country: " + address.Country);
            Console.WriteLine("Post Code : " + address.PostCode);
        }

        private void PrintAddress(Address address)
        {
            Console.WriteLine("This is Address");
            Console.WriteLine("Street : " + address.Street);
            Console.WriteLine("City : " + address.City);
            Console.WriteLine("State : " + address.State);
            Console.WriteLine("Country: " + address.Country);
            Console.WriteLine("Post Code : " + address.PostCode);
        }
    }
}
