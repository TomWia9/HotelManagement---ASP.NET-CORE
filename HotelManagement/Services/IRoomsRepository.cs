using HotelManagement.Data.DTO;
using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public interface IRoomsRepository
    {
        Task<Room> GetRoomAsync(int roomId);
        Task<bool> IsRoomExistsAsync(int roomId);
        Task<bool> UpdateRoomDataAsync(RoomDTO room);
    }
}
