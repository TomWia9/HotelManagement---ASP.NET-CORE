using HotelManagement.Data.DTO;
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

        public async Task<bool> IsRoomVacancyAsync(int roomId, DatesDTO dates)
        {
            //if in the bookings ther's no room with given id then this room is obviously vacancy
            if (!await _context.Bookings.AnyAsync(b => b.RoomId == roomId))
                return true;

            //if given dates colidate with current bookings then this room isn't vacany for these dates
            if (await _context.Bookings.Where(b => b.RoomId == roomId).AnyAsync(b => !( dates.CheckOutDate.Date < b.CheckInDate.Date || dates.CheckInDate.Date > b.CheckOutDate.Date)))
                return false;

            return true;

        }

        public async Task<bool> IsRoomExistsAsync(int roomId)
        {
            return await _context.Rooms.AnyAsync(r => r.Id == roomId);
        }
        public async Task<bool> IsBookingExistsAsync(int id)
        {
            return await _context.Bookings.AnyAsync(b => b.Id == id);
        }

        public async Task<bool> EditBookingDatesAsync(int bookingId, DatesDTO newDates)
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

        public bool AreDatesCorrect(DatesDTO newDates)
        {
            if (newDates == null)
                return false;

            return newDates.CheckInDate.Date > DateTime.Now && newDates.CheckOutDate.Date > newDates.CheckInDate.Date;
        }
    }
}
