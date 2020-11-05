using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.EntityConfiguration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.City)
              .IsRequired()
              .HasMaxLength(50);

            builder.Property(a => a.Street)
              .HasMaxLength(50);

            builder.Property(a => a.HouseNumber)
               .IsRequired()
               .HasMaxLength(8);

            builder.Property(a => a.PostCode)
               .IsRequired()
               .HasMaxLength(6);

        
        }
    }
}
