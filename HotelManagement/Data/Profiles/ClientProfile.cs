using AutoMapper;
using HotelManagement.Dto;
using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<AddressDto, Address>();
            CreateMap<ClientDto, Client>()
                .ForMember(c => c.Address, opt => opt.Ignore())
                .ForMember(c => c.AddressId, opt => opt.Ignore());
        }
    }
}
