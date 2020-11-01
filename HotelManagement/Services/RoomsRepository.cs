using HotelManagement.Data.DTO;
using HotelManagement.Models;
using HotelManagement.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public class RoomsRepository : IRoomsRepository
    {
        private readonly DatabaseContext _context;
        public RoomsRepository(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> IsRoomExistsAsync(int roomId)
        {
            return await _context.Rooms.AnyAsync(r => r.Id == roomId);
        }

        public async Task<Room> GetRoomAsync(int roomId)
        {
            return await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
        }
        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync(RoomsResourceParameters roomsResourceParameters)
        {
            if (roomsResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(roomsResourceParameters));
            }

            if (roomsResourceParameters.Balcony == null
                 && roomsResourceParameters.RoomType == null
                 && string.IsNullOrWhiteSpace(roomsResourceParameters.SearchQuery))
            {
                return await GetRoomsAsync();
            }

            var collection = _context.Rooms as IQueryable<Room>;

            if (!(roomsResourceParameters.Balcony == null))
            {
                var balcony = roomsResourceParameters.Balcony;
                collection = collection.Where(r => r.Balcony == balcony);
            }

            if (!(roomsResourceParameters.RoomType == null))
            {
                var roomType = roomsResourceParameters.RoomType;
                collection = collection.Where(r => r.Type == roomType);
            }

            if (!string.IsNullOrWhiteSpace(roomsResourceParameters.SearchQuery))
            {

                var searchQuery = roomsResourceParameters.SearchQuery.Trim();
                collection = collection.Where(r => r.Description.Contains(searchQuery));
            }

            return await collection.ToListAsync();
        }

        public async Task<bool> UpdateRoomDataAsync(RoomDTO room)
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
