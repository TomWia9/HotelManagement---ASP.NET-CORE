using System.Collections.Generic;

namespace HotelManagement.Models
{
    public class Room
    {
        public int Id { get; set; }
        public RoomType Type { get; set; }
        public bool HasBalcony { get; set; }
        public string Description { get; set; }
        public decimal PriceForDay { get; set; }
        public int MaxNumberOfPerson { get; set; }
        public List<Booking> Bookings { get; set; }

        //public string ImagePath { get; set; }

    }
}
