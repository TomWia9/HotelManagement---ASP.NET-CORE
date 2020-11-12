using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.Auth
{
    /// <summary>
    /// The administrator with Id, Email, FirstName, LastName and Token fields
    /// </summary>
    public class AuthenticateResponse
    {
        /// <summary>
        /// The id of the administrator
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The email of the administrator 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The first name of the administrator
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The last name of the administrator
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The token
        /// </summary>
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
