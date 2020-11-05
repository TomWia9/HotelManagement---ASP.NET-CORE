using HotelManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int? ClientId { get; set; }
        public int? RoomId { get; set; }
    }
}
