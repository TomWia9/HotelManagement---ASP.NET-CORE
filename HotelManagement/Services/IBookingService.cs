using HotelManagement.Data.Dto;
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
        Task<bool> CheckIfBookingExistsAsync(int id);
        Task<bool> CheckIfRoomIsVacancyAsync(int roomId);
        Task<bool> CheckIfRoomExistsAsync(int roomId);
        Task<bool> ChangeRoomVacancyStatusAsync(int roomId);
        Task<bool> CheckIfClientAlreadyHasABookingAsync(int clientId);
        Task<IEnumerable<Booking>> GetCurrentBookingsAsync();
        Task<bool> EditBookingDatesAsync(int bookingId, DatesDto newDates);
        bool CheckIfDatesAreCorrect(DatesDto newDates);
    }
}
