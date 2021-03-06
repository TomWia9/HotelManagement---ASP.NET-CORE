﻿namespace HotelManagement.Data.DTO
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
        /// The password of the administrator
        /// </summary>
        public string Password { get; set; }
    }
}
