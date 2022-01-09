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
    internal class CreditCardMap : IEntityTypeConfiguration<CreditCard>
    {

        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {

            builder
                .ToTable("CreditCards");

            builder
                .HasKey(x => x.CreditCardID)
                .HasName("CreditCardID");

            builder
                .Property(x => x.CreditCardName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar")
                .HasColumnName("CreditCardName");

            builder
                .Property(x => x.NameSurname)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar")
                .HasColumnName("NameSurname");

            builder
                .Property(x => x.CreditCardNo)
                .IsRequired()
                .HasMaxLength(16)
                .HasColumnType("char")
                .HasColumnName("CreditCardNo");

            builder
                .Property(x => x.ExpireDateYear)
                .IsRequired()
                .HasColumnName("ExpireDateYear");

            builder
                .Property(x => x.ExpireDateMounth)
                .IsRequired()
                .HasColumnName("ExpireDateMounth");

            builder
                .Property(x => x.CVC)
                .IsRequired()
                .HasColumnName("CVC");

            builder
                .HasOne(x => x.Customer)
                .WithMany(x => x.CreditCards)
                .HasForeignKey(x => x.CustomerID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

        }

    }
}
