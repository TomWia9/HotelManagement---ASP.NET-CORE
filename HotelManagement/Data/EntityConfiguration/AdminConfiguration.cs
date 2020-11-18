using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Data.EntityConfiguration
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Email)
              .IsRequired()
              .HasMaxLength(30);

            builder.Property(a => a.FirstName)
             .IsRequired()
             .HasMaxLength(25);

            builder.Property(a => a.LastName)
             .IsRequired()
             .HasMaxLength(25);

            builder.Property(a => a.Password)
             .IsRequired()
             .HasMaxLength(128);

        }
    }
}
