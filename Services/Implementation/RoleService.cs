using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Models.DTOs;
using Repos.Contracts;
using Services.Contracts;

namespace Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepo roleRepo;
        private readonly IMapper mapper;

        public RoleService(IRoleRepo roleRepo, IMapper mapper)
        {
            this.roleRepo = roleRepo;
            this.mapper = mapper;
        }



        public List<RoleDTO> GetAll()
        {
            return roleRepo.GetAll().Select(a => mapper.Map<RoleDTO>(a)).ToList();
        }
    }
}
