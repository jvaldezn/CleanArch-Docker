using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntityType
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.DateOfBirth)
                .IsRequired()
                .HasColumnType("DATE");

            builder.Property(u => u.Role)
                .IsRequired()
                .HasConversion<int>();

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Username).IsUnique();

            builder.HasData(
               new User { Id = 1, Username = "admin", Email = "admin@admin.com", Password = "$2a$11$0uZGUz4TAmUdSl6XdvtIjeUIL/v7IiJujaOvcBxDn0tJnXvsmsIwi", DateOfBirth = new DateTime(1990, 1, 1), Role = UserRole.Admin }
           );
        }
    }
}
