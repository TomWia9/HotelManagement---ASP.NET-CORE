using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.DTO
{
    public abstract class RoomForManipulationDTO
    {
        public RoomType Type { get; set; }
        public bool? Balcony { get; set; }
        public string Description { get; set; }
        public decimal PriceForDay { get; set; }

    }
}
