using AutoMapper;
using Models.DbModels;
using Models.DTOs;

namespace WeAreReading.Mappers
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap()
                .ForMember(dest => dest.Books, src => src.Ignore());
        }
    }
}
