using System.Collections.Generic;

namespace HotelManagement.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
