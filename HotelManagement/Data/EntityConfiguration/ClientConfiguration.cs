﻿using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Data.EntityConfiguration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Address)
                .WithOne(a => a.Client)
                .HasForeignKey<Address>(a => a.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Bookings)
                .WithOne(b => b.Client)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(c => c.FirstName)
              .IsRequired()
              .HasMaxLength(25);

            builder.Property(c => c.LastName)
              .IsRequired()
              .HasMaxLength(25);

            builder.Property(c => c.Email)
               .IsRequired()
               .HasMaxLength(50);

            builder.Property(c => c.PhoneNumber)
               .IsRequired()
               .HasMaxLength(17);

            builder.Property(c => c.Age)
               .IsRequired();

            builder.Property(c => c.Sex)
               .IsRequired()
               .HasConversion<string>();
        }
    }
}
