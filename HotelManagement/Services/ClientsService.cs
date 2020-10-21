using AutoMapper;
using HotelManagement.Dto;
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
        private readonly IMapper _mapper;

        public ClientsService(HotelManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Remove<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> ClientExists(int clientId)
        {
            return await _context.Clients.AnyAsync(c => c.Id == clientId);
        }

        public async Task<Client> GetClientAsync(int Id)
        {
            return await _context.Clients.Include(a => a.Address).FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateClientData(ClientDto client)
        {
            try
            {
                var clientToUpdate = _mapper.Map<Client>(client);
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
