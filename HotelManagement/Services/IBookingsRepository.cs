using HotelManagement.Data.DTO;
using HotelManagement.Models;
using HotelManagement.ResourceParameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public interface IBookingsRepository
    {
        Task<IEnumerable<Booking>> GetBookingsAsync();
        Task<IEnumerable<Booking>> GetBookingsAsync(BookingsResourceParameters bookingsResourceParameters);
        Task<Booking> GetBookingAsync(int id);
        Task<bool> IsBookingExistsAsync(int id);
        Task<bool> IsRoomVacancyAsync(int roomId, DatesDTO dates, int? bookingId = null);
        Task<bool> IsRoomExistsAsync(int roomId);
        Task<bool> EditBookingDatesAsync(int bookingId, DatesDTO newDates);
        Task<decimal> CalculateTotalPrice(int roomId, int numberOfPerson, DatesDTO dates);
        bool AreDatesCorrect(DatesDTO newDates);
        void UpdateBooking(Booking booking);
    }
}
