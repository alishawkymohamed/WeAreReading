using AutoMapper;
using Models.DbModels;
using Models.DTOs;

namespace WeAreReading.Mappers
{
    public class UserDtoProfile : Profile
    {
        public UserDtoProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.GovernmentName, src => src.MapFrom(x => x.Government.Name));
        }
    }
}
