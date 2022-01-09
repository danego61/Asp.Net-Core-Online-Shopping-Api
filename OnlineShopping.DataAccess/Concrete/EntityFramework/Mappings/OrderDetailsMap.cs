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
    internal class OrderDetailsMap : IEntityTypeConfiguration<OrderDetails>
    {

        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {

            builder
                .ToTable("OrderDetails");

            builder
                .HasKey(x => new { x.ProductID, x.OrderID });

            builder
                .Property(x => x.UnitPrice)
                .HasColumnName("UnitPrice")
                .IsRequired();

            builder
                .Property(x => x.Quantity)
                .HasColumnName("Quantity")
                .IsRequired();

            builder
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.OrderID)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductOrders)
                .HasForeignKey(x => x.ProductID)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

        }

    }
}
