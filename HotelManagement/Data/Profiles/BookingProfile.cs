using AutoMapper;
using HotelManagement.Data.DTO;
using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }
    }
}
