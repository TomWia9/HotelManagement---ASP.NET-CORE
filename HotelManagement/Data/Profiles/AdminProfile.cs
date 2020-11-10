using AutoMapper;
using HotelManagement.Data.DTO;
using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<AdminForCreationDTO, Admin>()
                .ForMember(a => a.Password,
                opt => opt.Ignore());

            CreateMap<AdminForUpdateDTO, Admin>()
                .ForMember(a => a.Password,
                opt => opt.Ignore());

            CreateMap<Admin, AdminDTO>();
        }
    }
}
