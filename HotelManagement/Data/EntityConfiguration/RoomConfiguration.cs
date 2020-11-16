using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.EntityConfiguration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasMany(r => r.Bookings)
                .WithOne(b => b.Room)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(r => r.Type)
              .IsRequired();

            builder.Property(r => r.HasBalcony)
              .IsRequired();

            builder.Property(r => r.Description)
             .IsRequired()
             .HasMaxLength(250);

            builder.Property(r => r.PriceForDay)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(r => r.MaxNumberOfPerson)
                .IsRequired();
        }
    }
}