using AutoMapper;
using System.Runtime;
using UserRegistrationAPI.Models;
using UserRegistrationAPI.Models.DTOs;

namespace UserRegistrationAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<UserCreateDTO, User>()
                .ForMember(dest => dest.Phones, opt => opt.MapFrom(src => src.Phones));
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<PhoneDTO, Phone>().ReverseMap();
        }
    }
}
