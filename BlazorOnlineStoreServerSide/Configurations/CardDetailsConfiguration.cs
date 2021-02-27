using BlazorOnlineStoreServerSide.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreServerSide.Configurations
{
    public class CardDetailsConfiguration : IEntityTypeConfiguration<CardDetail>
    {
        public void Configure(EntityTypeBuilder<CardDetail> builder)
        {
            builder.HasOne(x => x.Customer)
            .WithMany(X => X.CardDetails)
            .HasForeignKey(X => X.CustomerCardID);
        }
    }
}
