using HotelManagement.Dto;
using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public interface IClientsService
    {
        Task<Client> GetClientAsync(int clientId);
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<bool> IsClientExists(int clientId);
        Task<bool> UpdateClientData(ClientDto client);
    }
}
