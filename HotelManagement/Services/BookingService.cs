﻿using HotelManagement.Data.Dto;
using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public class BookingService : IBookingService
    {
        private readonly DatabaseContext _context;

        public BookingService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Booking> GetBookingAsync(int id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings.ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetCurrentBookingsAsync()
        {
            return await _context.Bookings.Where(b => b.CheckInDate <= DateTime.Now && b.CheckOutDate >= DateTime.Now).ToListAsync();
        }

        public async Task<bool> CheckIfRoomIsVacancyAsync(int roomId)
        {
            var query = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);

            return query.Vacancy;
        }

        public async Task<bool> CheckIfRoomExistsAsync(int roomId)
        {
            return await _context.Rooms.AnyAsync(r => r.Id == roomId);
        }

        public async Task<bool> ChangeRoomVacancyStatusAsync(int roomId)
        {
            try
            {
                var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
                room.Vacancy = !room.Vacancy;
                _context.Rooms.Update(room);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
                
        }

        public async Task<bool> CheckIfBookingExistsAsync(int id)
        {
            return await _context.Bookings.AnyAsync(b => b.Id == id);
        }

        public async Task<bool> CheckIfClientAlreadyHasABookingAsync(int clientId)
        {
            return await _context.Bookings.AnyAsync(b => b.ClientId == clientId);
        }

        public async Task<bool> EditBookingDatesAsync(int bookingId, DatesDto newDates)
        {
            try
            {
                var booking = await _context.Bookings.FindAsync(bookingId);
                booking.CheckInDate = newDates.CheckInDate;
                booking.CheckOutDate = newDates.CheckOutDate;
                _context.Update(booking);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckIfDatesAreCorrect(DatesDto newDates)
        {
            if (newDates == null)
                return false;

            return newDates.CheckInDate.Date > DateTime.Now && newDates.CheckOutDate.Date > newDates.CheckInDate.Date;
        }
    }
}
