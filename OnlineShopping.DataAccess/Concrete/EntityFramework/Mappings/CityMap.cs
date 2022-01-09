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
    internal class CityMap : IEntityTypeConfiguration<City>
    {

        public void Configure(EntityTypeBuilder<City> builder)
        {

            builder
                .ToTable("Cities");

            builder
                .HasKey(x => x.CityID)
                .HasName("CityID");

            builder
                .Property(x => x.CityName)
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .IsRequired()
                .HasColumnName("CityName");

        }

    }
}
