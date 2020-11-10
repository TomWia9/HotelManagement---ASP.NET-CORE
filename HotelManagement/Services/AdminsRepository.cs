using HotelManagement.Models;
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
    }
}
