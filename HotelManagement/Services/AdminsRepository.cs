using HotelManagement.Models;
using HotelManagement.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public class AdminsRepository : IAdminsRepository
    {
        private readonly DatabaseContext _context;
        public AdminsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Admin> GetAdminAsync(int adminId)
        {
            return await _context.Administrators.FindAsync(adminId);
        }

        public async Task<IEnumerable<Admin>> GetAdminsAsync()
        {
            return await _context.Administrators.ToListAsync();
        }

        public async Task<IEnumerable<Admin>> GetAdminsAsync(AdminsResourceParameters adminsResourceParameters)
        {
            if (adminsResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(adminsResourceParameters));
            }

            if (string.IsNullOrWhiteSpace(adminsResourceParameters.SearchQuery))
            {
                return await GetAdminsAsync();
            }

            var collection = _context.Administrators as IQueryable<Admin>;

            if (!string.IsNullOrWhiteSpace(adminsResourceParameters.SearchQuery))
            {

                var searchQuery = adminsResourceParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.FirstName.Contains(searchQuery)
                    || a.LastName.Contains(searchQuery)
                    || a.Email.Contains(searchQuery));
            }

            return await collection.ToListAsync();
        }

        public async Task<bool> IsEmailFree(string email)
        {
             return !await _context.Administrators.Where(a => a.Email == email).AnyAsync();
        }

        public void UpdateAdmin(Admin admin)
        {
            //no code in this implementation
        }
    }
}
