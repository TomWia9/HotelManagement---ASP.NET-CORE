using HotelManagement.Data.Dto;
using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public class RoomService : IRoomService
    {
        private readonly DatabaseContext _context;
        public RoomService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfRoomExistsAsync(int roomId)
        {
            return await _context.Rooms.AnyAsync(r => r.Id == roomId);
        }

        public async Task<Room> GetRoomAsync(int roomId)
        {
            return await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
        }

        public async Task<bool> UpdateRoomDataAsync(RoomDto room)
        {
            try
            {
                _context.Entry(await _context.Rooms.FirstOrDefaultAsync(r => r.Id == room.Id)).CurrentValues.SetValues(room);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
