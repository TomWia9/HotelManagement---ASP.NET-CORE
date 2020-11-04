using HotelManagement.DTO;
using HotelManagement.Models;
using HotelManagement.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public interface IClientsRepository
    {
        Task<Client> GetClientAsync(int clientId);
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<IEnumerable<Client>> GetClientsAsync(ClientsResourceParameters clientsResourceParameters);
        Task<bool> IsClientExistsAsync(int clientId);
        void UpdateClient(Client client);
    }
}
