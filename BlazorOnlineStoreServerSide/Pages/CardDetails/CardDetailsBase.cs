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

namespace BlazorOnlineStoreServerSide.Pages.CardDetails
{
    public class CardDetailsBase : ComponentBase
    {
        [Inject]
        public ICardDetailRepository CardDetailService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public int Id { get; set; }

        protected bool ShowFooter { get; set; } = false;

        protected bool HideFooter { get; set; } = true;

        public BillingAddress CardAddress { get; set; } = new BillingAddress();

        public CardDetail CardDetailT { get; set; } = new CardDetail();

        public CardDetailView CardDetail { get; set; } = new CardDetailView();

        public ConfirmDelete DeleteConfirmation { get; set; }

        protected async override Task OnInitializedAsync()
        {
            CardDetailT = await CardDetailService.GetById(Id);

            CardAddress = CardDetailT.BillingAddress;

            Mapper.Map(CardDetailT, CardDetail);

        }

        protected void DeleteClick()
        {
            DeleteConfirmation.Show();
        }

        protected async Task CardToDelete(bool deteConfirmed)
        {
            Mapper.Map(CardDetail, CardDetailT);

            if (deteConfirmed)
            {
                await CardDetailService.Delete(Id);

            }
            NavigationManager.NavigateTo("/cardDetailList");

        }

        protected void UpdateCard(int cardId)
        {
            NavigationManager.NavigateTo($"/editCardDetail/{cardId}");
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/cardDetailList");
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
