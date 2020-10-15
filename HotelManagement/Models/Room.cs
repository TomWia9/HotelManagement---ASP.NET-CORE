using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Models
{
    public class Room
    {
        public int Id { get; set; }
        public RoomType Type { get; set; }
        public bool Balcony { get; set; }
        public bool Vacancy { get; set; }
        public string Description { get; set; }

        //public string ImagePath { get; set; }

    }
}
