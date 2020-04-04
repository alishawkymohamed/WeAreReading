using AutoMapper;
using Models.DbModels;
using Models.DTOs;

namespace WeAreReading.Mappers
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<Role, RoleDTO>().ReverseMap()
                .ForMember(a => a.CreatedAt, src => src.Ignore())
                .ForMember(a => a.DeletedAt, src => src.Ignore())
                .ForMember(a => a.IsDeleted, src => src.Ignore())
                .ForMember(a => a.UserRoles, src => src.Ignore());
        }
    }
}
