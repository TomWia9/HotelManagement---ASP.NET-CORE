using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.DTO
{
    public abstract class AdminForManipulationDTO
    {
        /// <summary>
        /// The email of the administrator 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The first name of the administrator
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// the last name of the administrator
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// the password of the administrator
        /// </summary>
        public string Password { get; set; }
    }
}
