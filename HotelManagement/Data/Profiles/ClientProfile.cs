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
            CreateMap<AddressDto, Address>()
                .ForMember(a => a.Id, opt => opt.Ignore());
            CreateMap<Address, AddressDto>();
            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>()
                .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}
