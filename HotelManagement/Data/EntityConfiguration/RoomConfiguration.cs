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

            builder.HasOne(r => r.Booking)
                .WithOne(b => b.Room)
                .HasForeignKey<Booking>(b => b.RoomId);
                

            builder.Property(r => r.Type)
              .IsRequired();

            builder.Property(r => r.Balcony)
              .IsRequired();

            builder.Property(r => r.Vacancy)
             .IsRequired();

            builder.Property(r => r.Description)
             .IsRequired();
        }
    }
}