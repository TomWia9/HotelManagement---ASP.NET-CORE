using AutoMapper;
using HotelManagement.Data.DTO;
using HotelManagement.Models;

namespace HotelManagement.Data.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingDTO, Booking>();
            CreateMap<Booking, BookingDTO>();
            CreateMap<BookingForCreationDTO, Booking>()
                .ForMember(b => b.CheckInDate,
                opts => opts.MapFrom(src => src.BookingDates.CheckInDate))
                .ForMember(b => b.CheckOutDate,
                opts => opts.MapFrom(src => src.BookingDates.CheckOutDate));

            CreateMap<BookingForUpdateDTO, Booking>()
               .ForMember(b => b.CheckInDate,
               opts => opts.MapFrom(src => src.BookingDates.CheckInDate))
               .ForMember(b => b.CheckOutDate,
               opts => opts.MapFrom(src => src.BookingDates.CheckOutDate));

        }
    }
}
