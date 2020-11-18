using AutoMapper;
using HotelManagement.Data.DTO;
using HotelManagement.Models;

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
