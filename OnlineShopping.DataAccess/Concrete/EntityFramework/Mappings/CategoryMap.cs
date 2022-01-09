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
    internal class CategoryMap : IEntityTypeConfiguration<Category>
    {

        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder
                .ToTable("Categories");

            builder
                .HasKey(x => x.CategoryID)
                .HasName("CategoryID");

            builder
                .Property(x => x.CategoryName)
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .IsRequired()
                .HasColumnName("CategoryName");

            builder
                .HasIndex(x => x.CategoryName)
                .IsUnique();

            builder
                .Property(x => x.CategoryStatus)
                .HasConversion<int>()
                .IsRequired()
                .HasColumnName("CategoryStatus");

        }

    }
}
