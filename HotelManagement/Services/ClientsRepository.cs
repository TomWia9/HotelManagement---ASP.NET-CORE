using AutoMapper;
using HotelManagement.DTO;
using HotelManagement.Models;
using HotelManagement.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> IsClientExistsAsync(int clientId)
        {
            return await _context.Clients.AnyAsync(c => c.Id == clientId);
        }

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            return await _context.Clients.Include(a => a.Address).ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetClientsAsync(ClientsResourceParameters clientsResourceParameters)
        {
            if (clientsResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(clientsResourceParameters));
            }

            if (clientsResourceParameters.Sex == null
                 && string.IsNullOrWhiteSpace(clientsResourceParameters.SearchQuery))
            {
                return await GetClientsAsync();
            }

            var collection = _context.Clients.Include(c => c.Address) as IQueryable<Client>;

            if (!(clientsResourceParameters.Sex == null))
            {
                var sex = clientsResourceParameters.Sex;
                collection = collection.Where(c => c.Sex == sex);
            }

            if (!string.IsNullOrWhiteSpace(clientsResourceParameters.SearchQuery))
            {

                var searchQuery = clientsResourceParameters.SearchQuery.Trim();
                collection = collection.Where(c => c.FirstName.Contains(searchQuery)
                    || c.LastName.Contains(searchQuery)
                    || c.LastName.Contains(searchQuery)
                    || c.PhoneNumber.Contains(searchQuery)
                    || c.Email.Contains(searchQuery));
            }

            return await collection.ToListAsync();
        }

        public async Task<Client> GetClientAsync(int clientId)
        {
            return await _context.Clients.Include(a => a.Address).FirstOrDefaultAsync(c => c.Id == clientId);
        }

        public async Task<bool> UpdateClientDataAsync(ClientDTO client)
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
