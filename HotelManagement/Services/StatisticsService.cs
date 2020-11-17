using AutoMapper;
using HotelManagement.Data.DTO;
using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public StatisticsService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable> GetClientsByBookingsNumbersAsync(uint? numberOfClients = null)
        {
            IEnumerable<Client> clients;

            if (numberOfClients == null)
            {
                clients = await _context.Clients.Include(c => c.Address).Include(c => c.Bookings).Where(c => c.Bookings.Count > 0).OrderByDescending(c => c.Bookings.Count).ToListAsync();
            }

            else
            {
                clients = await _context.Clients.Include(c => c.Address).Include(c => c.Bookings).Where(c => c.Bookings.Count > 0).OrderByDescending(c => c.Bookings.Count).Take((int)numberOfClients).ToListAsync();

            }

            return clients.Select(c => new { Client = _mapper.Map<ClientDTO>(c), NumberOfBookings = c.Bookings.Count });
        }

        public async Task<IEnumerable> GetRoomsByPopularityAsync(uint? numberOfRooms = null)
        {
            IEnumerable<Room> rooms;

            if (numberOfRooms == null)
            {
                rooms = await _context.Rooms.Include(r => r.Bookings).Where(r => r.Bookings.Count > 0).OrderByDescending(r => r.Bookings.Count).ToListAsync();
            }

            else
            {
                rooms = await _context.Rooms.Include(r => r.Bookings).Where(r => r.Bookings.Count > 0).OrderByDescending(r => r.Bookings.Count).Take((int)numberOfRooms).ToListAsync();

            }

            return rooms.Select(r => new { Room = _mapper.Map<RoomDTO>(r), NumberOfBookings = r.Bookings.Count });
        }

        public async Task<decimal> GetTotalEarnedMoneyAsync(uint? peroid = null)
        {
            if (peroid == null)
            {
                return await _context.Bookings.SumAsync(b => b.TotalPrice);
            }

            else
            {
                return await _context.Bookings.Where(b => b.CheckOutDate.Date > DateTime.Now.AddDays((int)-peroid).Date).SumAsync(b => b.TotalPrice);
            }

        }
    }
}
