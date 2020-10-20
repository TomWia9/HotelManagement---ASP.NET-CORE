using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.Dto
{
    public class RoomDto
    {
        public RoomType Type { get; set; }
        public bool Balcony { get; set; }
        public bool Vacancy { get; set; }
        public string Description { get; set; }
    }
}
