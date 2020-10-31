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
            CreateMap<NewBookingDTO, DatesDTO>();
            CreateMap<BookingDTO, Booking>();
            CreateMap<Booking, BookingDTO>();
            CreateMap<NewBookingDTO, Booking>();
        }
    }
}
