using BlazorOnlineStoreServerSide.Configurations;
using BlazorOnlineStoreServerSide.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorOnlineStoreServerSide.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AddressEntityConfiguration());
            builder.ApplyConfiguration(new BillingAddressEntityConfiguration());
            builder.ApplyConfiguration(new CardDetailsConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new OrderLineEntityConfiguration());

            base.OnModelCreating(builder);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<BillingAddress> BillingAddresses { get; set; }
        public DbSet<CardDetail> CardDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLineItem> OrderLineItems { get; set; }
        public DbSet<Product> Products { get; set; }


    }
}
