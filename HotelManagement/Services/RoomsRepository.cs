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
        private readonly IBookingsRepository _bookingsRepository;
        public RoomsRepository(DatabaseContext context, IBookingsRepository bookingsRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _bookingsRepository = bookingsRepository;
        }

        public async Task<bool> IsRoomExistsAsync(int roomId)
        {
            return await _context.Rooms.AnyAsync(r => r.Id == roomId);
        }

        public async Task<Room> GetRoomAsync(int roomId)
        {
            return await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
        }

        private async Task<IEnumerable<int>> GetVacancyRoomsAsync(DatesDTO dates)
        {
            var rooms = await GetRoomsAsync();
            List<int> roomsIds = new List<int>();

            foreach (var room in rooms)
            {
                if(await _bookingsRepository.IsRoomVacancyAsync(room.Id, dates))
                {
                    roomsIds.Add(room.Id);
                }
            }

            return roomsIds;
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
                 && roomsResourceParameters.VacancyInDays == null
                 && roomsResourceParameters.PriceLessThan == null
                 && roomsResourceParameters.NumberOfPerson == null
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

            if (!(roomsResourceParameters.PriceLessThan == null))
            {
                collection = collection.Where(r => r.PriceForDay < roomsResourceParameters.PriceLessThan);
            }

            if (!(roomsResourceParameters.NumberOfPerson == null))
            {
                collection = collection.Where(r => r.MaxNumberOfPerson == roomsResourceParameters.NumberOfPerson);
            }

            if (!(roomsResourceParameters.VacancyInDays == null))
            {
                if (!_bookingsRepository.AreDatesCorrect(roomsResourceParameters.VacancyInDays))
                {
                    return null;
                }
                   
                var roomsIds = await GetVacancyRoomsAsync(roomsResourceParameters.VacancyInDays);
                collection = collection.Where(r => roomsIds.Contains(r.Id));
            }

            if (!string.IsNullOrWhiteSpace(roomsResourceParameters.SearchQuery))
            {

                var searchQuery = roomsResourceParameters.SearchQuery.Trim();
                collection = collection.Where(r => r.Description.Contains(searchQuery));
            }

            return await collection.ToListAsync();
        }

        public void UpdateRoom(Room room)
        {
            //no code in this implementation
        }


    }
}
