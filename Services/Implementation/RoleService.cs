using AutoMapper;
using Models.DbModels;
using Models.DTOs;
using Repos.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return this.roleRepo.GetAll().Select(a=> this.mapper.Map<RoleDTO>(a)).ToList();
        }
    }
}
