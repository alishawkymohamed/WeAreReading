using AutoMapper;
using Models.DbModels;
using Models.DTOs;

namespace WeAreReading.Mappers
{
    public class BookMapper : Profile
    {
        public BookMapper()
        {
            CreateMap<BookDTO, Book>()
                .ReverseMap()
                .ForMember(dest => dest.CategoryName, src => src.MapFrom(x => x.Category.Name))
                .ForMember(dest => dest.OwnerName, src => src.MapFrom(x => x.User.FullName));

            CreateMap<Book, InsertBookDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Category, src => src.Ignore())
                .ForMember(dest => dest.User, src => src.Ignore());
        }
    }
}
