using AutoMapper;
using HotelManagement.Data.Dto;
using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomDto, Room>();
            CreateMap<NewRoomDto, Room>();
            CreateMap<Room, RoomDto>();

        }
    }
}
