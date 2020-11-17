using HotelManagement.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public interface IStatisticsService
    {
        Task<IEnumerable> GetRoomsByPopularityAsync(uint? numberOfRooms = null);
        Task<IEnumerable> GetClientsByBookingsNumbersAsync(uint? numberOfClients = null);
        Task<decimal> GetTotalEarnedMoneyAsync(uint? peroid = null);
    }
}
