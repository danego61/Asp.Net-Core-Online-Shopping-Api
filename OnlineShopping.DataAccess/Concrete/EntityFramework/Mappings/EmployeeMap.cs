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
    internal class EmployeeMap : IEntityTypeConfiguration<Employee>
    {

        public void Configure(EntityTypeBuilder<Employee> builder)
        {

            builder
                .ToTable("Empoyees");

            builder
               .Property(x => x.EmployeeStatus)
               .HasConversion<int>()
               .IsRequired()
               .HasColumnName("EmployeeStatus");

            builder
               .Property(x => x.EmployeeType)
               .HasConversion<int>()
               .IsRequired()
               .HasColumnName("EmployeeType");

        }

    }
}
