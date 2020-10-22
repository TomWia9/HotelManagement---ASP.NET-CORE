using HotelManagement.Data.Dto;
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
        private readonly HotelManagementContext _context;

        public BookingService(HotelManagementContext context)
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
    }
}
