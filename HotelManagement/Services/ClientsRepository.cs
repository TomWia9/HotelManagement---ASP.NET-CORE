using AutoMapper;
using HotelManagement.DTO;
using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public class ClientsRepository: IClientsRepository
    {
        private readonly DatabaseContext _context;

        public ClientsRepository(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> IsClientExists(int clientId)
        {
            return await _context.Clients.AnyAsync(c => c.Id == clientId);
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _context.Clients.Include(a => a.Address).ToListAsync();
        }

        public async Task<Client> GetClientAsync(int clientId)
        {
            return await _context.Clients.Include(a => a.Address).FirstOrDefaultAsync(c => c.Id == clientId);
        }

        public async Task<bool> UpdateClientData(ClientDTO client)
        {
            try
            {
                _context.Entry(await _context.Clients.FirstOrDefaultAsync(c => c.Id == client.Id)).CurrentValues.SetValues(client);
                _context.Entry(await _context.Addresses.FirstOrDefaultAsync(a => a.ClientId == client.Id)).CurrentValues.SetValues(client.Address);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
