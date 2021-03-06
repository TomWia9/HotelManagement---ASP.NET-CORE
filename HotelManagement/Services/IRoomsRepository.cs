﻿using HotelManagement.Models;
using HotelManagement.ResourceParameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public interface IRoomsRepository
    {
        Task<Room> GetRoomAsync(int roomId);
        Task<IEnumerable<Room>> GetRoomsAsync();
        Task<IEnumerable<Room>> GetRoomsAsync(RoomsResourceParameters roomsResourceParameters);
        Task<bool> IsRoomExistsAsync(int roomId);
        void UpdateRoom(Room room);
    }
}
