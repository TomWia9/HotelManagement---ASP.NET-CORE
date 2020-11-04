using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.DTO
{
    public class BookingForCreationDTO
    {
        public DatesDTO BookingDates { get; set; }
        public int ClientId { get; set; }
        public int RoomId { get; set; }
    }
}
