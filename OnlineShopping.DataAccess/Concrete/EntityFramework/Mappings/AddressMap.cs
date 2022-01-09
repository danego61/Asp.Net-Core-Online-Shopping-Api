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
    internal class AddressMap : IEntityTypeConfiguration<Address>
    {

        public void Configure(EntityTypeBuilder<Address> builder)
        {
            
            builder
                .ToTable("Addresses");

            builder
                .HasKey(x => x.AddressID)
                .HasName("AddressID");

            builder
                .Property(x => x.AddressName)
                .HasMaxLength(100)
                .HasColumnType("nvarchar")
                .IsRequired()
                .HasColumnName("AddressName");

            builder
                .Property(x => x.AddressDescription)
                .HasMaxLength(150)
                .HasColumnType("nvarchar")
                .IsRequired()
                .HasColumnName("AddressDescription");

            builder
                .HasOne(x => x.Customer)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.CustomerID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .HasOne(x => x.Region)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.RegionID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

        }

    }
}
