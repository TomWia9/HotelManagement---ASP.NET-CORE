﻿using AutoMapper;
using HotelManagement.Data.DTO;
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
            CreateMap<RoomDTO, Room>();
            CreateMap<RoomForCreationDTO, Room>();
            CreateMap<Room, RoomDTO>();
            CreateMap<RoomForUpdateDTO, Room>();
            CreateMap<Room, RoomForUpdateDTO>();

        }
    }
}
