using BlazorOnlineStoreServerSide.Contracts;
using BlazorOnlineStoreServerSide.ViewModels;
using BlazorOnlineStoreServerSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Helpers
{
    public class AddCardAndAddressToCustomerInfo : IAddCardAndAddressToCustomerInfo
    {
        private List<AddressView> Addresses = new List<AddressView>();
        private List<CardDetailView> cardDetails = new List<CardDetailView>();

        public bool ClearAddresses { get; set; } = false;
        public bool ClearCardDetails { get; set; } = false;
        

        public event Action OnChange;

        public List<AddressView> AddAddressToCustomerInfo(AddressView address)
        {
            Addresses.Add(
            new AddressView 
            { 
               Street = address.Street,
               City = address.City,
               State = address.State,
               Country = address.Country,
               PostCode = address.PostCode,
               IsBillingAddress = address.IsBillingAddress,
               IsHomeAddress = address.IsHomeAddress,
               IsShippingAddress = address.IsShippingAddress
                                    
            });

            NotifyDataChanged();

            return Addresses;
        }

        public List<CardDetailView> AddCardDetailsToCustomerInfo(CardDetailView card, BillingAddress address)
        {
            cardDetails.Add(
            new CardDetailView
            {
                NameOnCard = card.NameOnCard,
                CardNumber = card.CardNumber,
                CardSecurityNumber = card.CardSecurityNumber,
                ExpiryDate = card.ExpiryDate,
                BillingAddress = address
            });
          
            NotifyDataChanged();

            return cardDetails;
        }

        public void ClearList()
        {
            if (ClearAddresses) Addresses.Clear();
            if (ClearCardDetails) cardDetails.Clear();

            NotifyDataChanged();
        }

        private void NotifyDataChanged() => OnChange.Invoke();       

    }

}
