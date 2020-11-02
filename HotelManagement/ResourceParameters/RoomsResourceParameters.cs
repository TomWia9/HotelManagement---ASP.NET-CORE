using HotelManagement.Data.DTO;
using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.ResourceParameters
{
    public class RoomsResourceParameters : ResourceParameters
    {
        public bool? Balcony { get; set; }
        public RoomType? RoomType { get; set; }
        public DatesDTO VacancyInDays { get; set; }
    }
}
