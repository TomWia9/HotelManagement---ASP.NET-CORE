using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.Auth
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(Admin admin, string token)
        {
            Id = admin.Id;
            Email = admin.Email;
            FirstName = admin.FirstName;
            LastName = admin.LastName;
            Token = token;
        }
    }
}
