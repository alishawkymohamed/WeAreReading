using AutoMapper;
using Models.DbModels;
using Models.DTOs;

namespace WeAreReading.Mappers
{
    public class StatusMapper : Profile
    {
        public StatusMapper()
        {
            CreateMap<Status, StatusDTO>().ReverseMap()
                .ForMember(dest => dest.Books, src => src.Ignore());
        }
    }
}
