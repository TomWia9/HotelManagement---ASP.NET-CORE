using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.EntityConfiguration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasOne(b => b.Room)
                .WithMany(r => r.Bookings);

            builder.HasOne(b => b.Client)
                .WithMany(c => c.Bookings);

            builder.Property(b => b.CheckInDate)
              .IsRequired()
              .HasColumnType("date")
              .HasMaxLength(20);

            builder.Property(b => b.CheckOutDate)
              .IsRequired()
              .HasColumnType("date")
              .HasMaxLength(20);
        }
    }
}
