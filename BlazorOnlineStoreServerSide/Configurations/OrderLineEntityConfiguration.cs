using BlazorOnlineStoreServerSide.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Configurations
{
    public class OrderLineEntityConfiguration : IEntityTypeConfiguration<OrderLineItem>
    {
        public void Configure(EntityTypeBuilder<OrderLineItem> builder)
        {
            builder.HasOne(x => x.Order)
            .WithMany(x => x.OrderLineItems)
            .HasForeignKey(x => x.OrderID);
        }
    }
}
