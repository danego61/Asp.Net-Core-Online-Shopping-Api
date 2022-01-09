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
    internal class UserMap : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            
            builder
                .ToTable("Users");

            builder
                .HasKey(x => x.UserID);

            builder
                .Property(x => x.Name)
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .HasColumnName("Name")
                .IsRequired();

            builder
                .Property(x => x.Surname)
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .HasColumnName("Surname")
                .IsRequired();

            builder
                .Property(x => x.Email)
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .HasColumnName("Email")
                .IsRequired();

            builder
                .HasIndex(x => x.Email)
                .IsUnique();

            builder
                .Property(x => x.Password)
                .HasColumnName("Password")
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnType("char");

            builder
                .Property(x => x.EmailVerifyToken)
                .HasColumnName("EmailVerifyToken");

            builder
                .Property(x => x.IsEmailVerified)
                .HasColumnName("EmailVerified");

            builder
                .Property(x => x.IsPasswordChange)
                .HasColumnName("PasswordChange");

            builder
               .Ignore(x => x.NameSurname);

        }

    }
}
