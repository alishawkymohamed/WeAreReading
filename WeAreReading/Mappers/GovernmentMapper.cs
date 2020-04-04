using AutoMapper;
using Models.DbModels;
using Models.DTOs;

namespace WeAreReading.Mappers
{
    public class GovernmentMapper : Profile
    {
        public GovernmentMapper()
        {
            CreateMap<Government, GovernmentDTO>().ReverseMap()
                .ForMember(a => a.CreatedAt, src => src.Ignore());
        }
    }
}
