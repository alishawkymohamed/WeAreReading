using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Models.DTOs;
using Repos.Contracts;
using Services.Contracts;

namespace Services.Implementation
{
    public class GovernmentService : IGovernmentService
    {
        private readonly IGovernmentRepo GovernmentRepo;
        private readonly IMapper mapper;

        public GovernmentService(IGovernmentRepo GovernmentRepo, IMapper mapper)
        {
            this.GovernmentRepo = GovernmentRepo;
            this.mapper = mapper;
        }



        public List<GovernmentDTO> GetAll()
        {
            return GovernmentRepo.GetAll().Select(a => mapper.Map<GovernmentDTO>(a)).ToList();
        }
    }
}
