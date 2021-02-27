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
    public class AddCardDetailBase : ComponentBase
    {
        [Inject]
        public ICardDetailRepository  CardDetailService{ get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public BillingAddress CardAddress { get; set; } = new BillingAddress();

        public CardDetail CardDetailT { get; set; } = new CardDetail();

        public CardDetailView CardDetail { get; set; } = new CardDetailView();

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        protected async Task CreateCardDetail()
        {
            Mapper.Map(CardDetail, CardDetailT);
            
            CardDetailT.BillingAddress = CardAddress;            
            
            var result = await CardDetailService.Add(CardDetailT);

            if (result != null)
            {
                NavigationManager.NavigateTo("/cardDetailList");
            }

        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/cardDetailList");
        }

    }
}
