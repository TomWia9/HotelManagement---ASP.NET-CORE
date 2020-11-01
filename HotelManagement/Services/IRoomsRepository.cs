using HotelManagement.Data.DTO;
using HotelManagement.Models;
using HotelManagement.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public interface IRoomsRepository
    {
        Task<Room> GetRoomAsync(int roomId);
        Task<IEnumerable<Room>> GetRoomsAsync();
        Task<IEnumerable<Room>> GetRoomsAsync(RoomsResourceParameters roomsResourceParameters);
        Task<bool> IsRoomExistsAsync(int roomId);
        Task<bool> UpdateRoomDataAsync(RoomDTO room);
    }
}
