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
    internal class RegionMap : IEntityTypeConfiguration<Region>
    {

        public void Configure(EntityTypeBuilder<Region> builder)
        {
            
            builder
                .ToTable("Regions");

            builder
                .HasKey(x => x.RegionID)
                .HasName("RegionID");

            builder
                .Property(x => x.RegionName)
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .IsRequired()
                .HasColumnName("RegionName");

            builder
                .HasOne(x => x.City)
                .WithMany(x => x.Regions)
                .HasForeignKey(x => x.CityID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

        }

    }
}
