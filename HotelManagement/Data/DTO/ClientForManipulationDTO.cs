using HotelManagement.Models;

namespace HotelManagement.Data.DTO
{
    public abstract class ClientForManipulationDTO
    {
        /// <summary>
        /// The first name of the client
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The last name of the client
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The sex (gender) of the client
        /// </summary>
        public Sex Sex { get; set; }
        /// <summary>
        /// The age of the client
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// The phone number of the client
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// The email of the client
        /// </summary>
        public string Email { get; set; }
        public AddressDTO Address { get; set; }
    }
}
