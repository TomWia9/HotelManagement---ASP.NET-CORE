using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public class ClientsService: IClientsService
    {
        private readonly HotelManagementContext _context;

        public ClientsService(HotelManagementContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public async Task<Client> GetClientAsync(int Id)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
