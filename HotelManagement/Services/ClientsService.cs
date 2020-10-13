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

        public ClientsService(HotelManagementContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        //This method returns addressId if address already exists in database or creates new address and returns it Id
        public async Task<int> CreateAddress(AddressDto address)
        {
            var query =  await _context.Addresses.FirstOrDefaultAsync(a => a.City == address.City
                && a.Street == address.Street
                && a.HouseNumber == address.HouseNumber
                && a.PostCode == address.PostCode);

            if(query == null)
            {
                var newAddress = new Address()
                {
                    City = address.City,
                    Street = address.Street,
                    HouseNumber = address.HouseNumber,
                    PostCode = address.PostCode,
                };

                Add(newAddress);
                await _context.SaveChangesAsync();
                query = await _context.Addresses.FirstOrDefaultAsync(a => a.City == address.City
                && a.Street == address.Street
                && a.HouseNumber == address.HouseNumber
                && a.PostCode == address.PostCode);
                return query.Id;
            }
            else
            {
                return query.Id;
            }

        }

        public async Task<Client> GetClientAsync(int Id)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
