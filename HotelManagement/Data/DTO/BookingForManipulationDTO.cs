using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.DTO
{
    public abstract class BookingForManipulationDTO
    {
        public DatesDTO BookingDates { get; set; }
        public int NumberOfPerson { get; set; }
        public int ClientId { get; set; }
        public int RoomId { get; set; }
    }
}
