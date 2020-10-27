﻿using AutoMapper;
using HotelManagement.Dto;
using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public class ClientsService: IClientsService
    {
        private readonly DatabaseContext _context;

        public ClientsService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfClientExists(int clientId)
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

        public async Task<bool> UpdateClientData(ClientDto client)
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
