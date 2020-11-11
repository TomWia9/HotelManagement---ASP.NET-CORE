using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.DTO
{
    /// <summary>
    /// The administrator with Id, Email, FirstName and LastName fields
    /// </summary>
    public class AdminDTO
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
    }
}
