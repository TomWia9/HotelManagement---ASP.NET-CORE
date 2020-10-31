using HotelManagement.Data.DTO;
using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingAsync(int id);
        Task<bool> IsBookingExistsAsync(int id);
        Task<bool> IsRoomVacancyAsync(int roomId, DatesDTO dates);
        Task<bool> IsRoomExistsAsync(int roomId);
        Task<IEnumerable<Booking>> GetCurrentBookingsAsync();
        Task<bool> EditBookingDatesAsync(int bookingId, DatesDTO newDates);
        bool AreDatesCorrect(DatesDTO newDates);
    }
}
