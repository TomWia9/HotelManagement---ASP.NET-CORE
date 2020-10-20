using HotelManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.Dto
{
    public class BookingDto
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int ClientId { get; set; }

        public int RoomId { get; set; }
    }
}
