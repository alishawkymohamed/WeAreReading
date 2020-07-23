using AutoMapper;
using Models.DbModels;
using Models.DTOs;

namespace WeAreReading.Mappers
{
    public class RequestMapper : Profile
    {
        public RequestMapper()
        {
            CreateMap<Request, RequestDTO>()
                .ForMember(dest => dest.BookName, src => src.MapFrom(x => x.Book.Title))
                .ForMember(dest => dest.SenderName, src => src.MapFrom(x => x.Sender.FullName))
                .ForMember(dest => dest.ReceiverName, src => src.MapFrom(x => x.Receiver.FullName));

            CreateMap<CreateRequestDTO, Request>()
                .ForMember(dest => dest.Book, src => src.Ignore())
                .ForMember(dest => dest.Sender, src => src.Ignore())
                .ForMember(dest => dest.IsAccepted, src => src.Ignore())
                .ForMember(dest => dest.Id, src => src.Ignore())
                .ForMember(dest => dest.Receiver, src => src.Ignore());
        }
    }
}
