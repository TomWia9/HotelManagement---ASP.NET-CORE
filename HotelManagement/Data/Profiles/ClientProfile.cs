using AutoMapper;
using HotelManagement.Data.DTO;
using HotelManagement.Models;

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
            CreateMap<Client, ClientForUpdateDTO>();

        }
    }
}
