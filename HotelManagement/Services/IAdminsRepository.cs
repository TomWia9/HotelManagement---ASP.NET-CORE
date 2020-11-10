using HotelManagement.Models;
using HotelManagement.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public interface IAdminsRepository
    {
        Task<Admin> GetAdminAsync(int adminId);
        Task<IEnumerable<Admin>> GetAdminsAsync();
        Task<IEnumerable<Admin>> GetAdminsAsync(AdminsResourceParameters adminsResourceParameters);
        Task<bool> IsEmailFree(string email);
        void UpdateAdmin(Admin admin);
    }
}
