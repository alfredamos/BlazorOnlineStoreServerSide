using BlazorOnlineStoreServerSide.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BlazorOnlineStoreServerSide.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {                        
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product
            {
                ProductID = 1,
                Brand = "Dolce Gabana",
                Name = "Denim Shoe",
                Price = 45.0,
                ImageLink = "",
                Description = "Shiny Bright Shoe"
            },
            new Product
            {
                ProductID = 2,
                Brand = "Givenchy",
                Name = "Rolex Belt",
                Price = 65.0,
                ImageLink = "",
                Description = "Rolex Belt"
            },
            new Product
            {
                ProductID = 3,
                Brand = "Plasmot Special",
                Name = "Shirt and Shoe Combo",
                Price = 70.0,
                ImageLink = "",
                Description = "Shirt & Shoe"
            },
            new Product
            {
                ProductID = 4,
                Brand = "Romano Sport",
                Name = "Greeno Sport Shoe",
                Price = 65.0,
                ImageLink = "",
                Description = "Sport Shoe"
            }
            );
        }
    }
}
