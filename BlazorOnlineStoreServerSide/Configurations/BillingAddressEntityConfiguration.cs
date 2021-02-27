using BlazorOnlineStoreServerSide.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Configurations
{
    public class BillingAddressEntityConfiguration : IEntityTypeConfiguration<BillingAddress>
    {
        public void Configure(EntityTypeBuilder<BillingAddress> builder)
        {
            builder.HasOne(s => s.Card)
            .WithOne(ad => ad.BillingAddress)
            .HasForeignKey<BillingAddress>(ad => ad.CardAddressID);
        }
    }
}
