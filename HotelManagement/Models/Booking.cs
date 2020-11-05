using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfPerson { get; set; }
        public decimal TotalPrice { get; set; }
        public int? ClientId { get; set; }
        public Client Client { get; set; }
        public int? RoomId { get; set; }
        public Room Room { get; set; }
    }
}
