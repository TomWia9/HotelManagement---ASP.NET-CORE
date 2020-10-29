using HotelManagement.Data.Dto;
using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public interface IRoomService
    {
        Task<Room> GetRoomAsync(int roomId);
        Task<bool> IsRoomExistsAsync(int roomId);
        Task<bool> UpdateRoomDataAsync(RoomDto room);
    }
}
