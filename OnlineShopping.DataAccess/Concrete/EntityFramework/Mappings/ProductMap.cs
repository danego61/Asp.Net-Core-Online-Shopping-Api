using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopping.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.DataAccess.Concrete.EntityFramework.Mappings
{
    internal class ProductMap : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder
                .ToTable("Products");

            builder
                .HasKey(x => x.ProductID)
                .HasName("ProductID");

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .HasColumnName("Name");

            builder
                .HasIndex(x => x.Name)
                .IsUnique();

            builder
                .Property(x => x.Price)
                .IsRequired()
                .HasColumnName("Price");

            builder
                .Property(x => x.DiscountedPrice)
                .HasColumnName("DiscountedPrice");

            builder
                .Property(x => x.Explanation)
                .HasMaxLength(150)
                .HasColumnType("nvarchar")
                .HasColumnName("Explanation")
                .IsRequired();

            builder
                .Property(x => x.Image)
                .HasColumnType("image")
                .HasColumnName("Image");

            builder
                .Property(x => x.ProductStatus)
                .HasConversion<int>()
                .IsRequired()
                .HasColumnName("ProductStatus");

            builder
                .HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

        }

    }
}
