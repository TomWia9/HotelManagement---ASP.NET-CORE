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
        void Add<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<Client> GetClientAsync(int Id);
        Task<bool> ClientExists(int clientId);
        Task<bool> UpdateClientData(ClientDto client);
    }
}
