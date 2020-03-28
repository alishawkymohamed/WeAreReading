using Models.DbModels;
using Models.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeAreReading.Mappers
{
    public class RoleMapper: Profile
    {
        public RoleMapper()
        {
            CreateMap<Role, RoleDTO>().ReverseMap()
                .ForMember(a=>a.CreateAt, src => src.Ignore())
                .ForMember(a=>a.DeletedAt,src => src.Ignore())
                .ForMember(a=>a.IsDeleted, src => src.Ignore())
                .ForMember(a=>a.UserRoles, src => src.Ignore());
        }
    }
}
