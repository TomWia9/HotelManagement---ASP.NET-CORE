using HotelManagement.Data.DTO;
using HotelManagement.Models;

namespace HotelManagement.ResourceParameters
{
    public class RoomsResourceParameters : ResourceParameters
    {
        public bool? HasBalcony { get; set; }
        public RoomType? RoomType { get; set; }
        public decimal? PriceLessThan { get; set; }
        public int? NumberOfPerson { get; set; }
        public DatesDTO VacancyInDays { get; set; }
    }
}
