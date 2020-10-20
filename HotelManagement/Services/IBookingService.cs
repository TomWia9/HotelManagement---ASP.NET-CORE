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
        void Add<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Booking>> GetAllBookings();
        Task<Booking> GetBooking(int id);
        Task<bool> CheckIfRoomIsVacancy(int roomId);
        Task<bool> CheckIfRoomExists(int roomId);
        Task<bool> ChangeRoomVacancyStatus(int roomId);
    }
}
