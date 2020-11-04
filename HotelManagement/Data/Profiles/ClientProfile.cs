using AutoMapper;
using HotelManagement.Data.DTO;
using HotelManagement.DTO;
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
            CreateMap<AddressDTO, Address>()
                .ForMember(a => a.Id, opt => opt.Ignore());
            CreateMap<Address, AddressDTO>();
            CreateMap<Client, ClientDTO>();
            CreateMap<ClientDTO, Client>();
            CreateMap<ClientForCreationDTO, Client>();
            CreateMap<ClientForUpdateDTO, Client>();

        }
    }
}
