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
    internal class OrderMap : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder
                .ToTable("Orders");

            builder
                .HasKey(x => x.OrderID)
                .HasName("OrderID");

            builder
                .Property(x => x.CreditCardNo)
                .IsRequired()
                .HasMaxLength(16)
                .HasColumnType("char")
                .HasColumnName("CreditCardNo");

            builder
                .Property(x => x.ShipCity)
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .IsRequired()
                .HasColumnName("ShipCity");

            builder
                .Property(x => x.ShipRegion)
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .IsRequired()
                .HasColumnName("ShipRegion");

            builder
                .Property(x => x.ShipAddress)
                .HasMaxLength(150)
                .HasColumnType("nvarchar")
                .IsRequired()
                .HasColumnName("ShipAddress");

            builder
                .Property(x => x.OrderDate)
                .HasColumnName("OrderDate")
                .IsRequired();

            builder
                .Property(x => x.ShippedDate)
                .HasColumnName("ShippedDate");

            builder
              .Property(x => x.OrderPreparationStatus)
              .HasConversion<int>()
              .IsRequired()
              .HasColumnName("OrderPreparationStatus");

            builder
              .Property(x => x.OrderShippingStatus)
              .HasConversion<int>()
              .IsRequired()
              .HasColumnName("OrderShippingStatus");

            builder
                .HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(x => x.CustomerID)
                .IsRequired();

        }

    }
}
