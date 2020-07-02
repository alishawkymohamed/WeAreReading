using AutoMapper;
using Models.DTOs;
using Repos.Contracts;
using Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Services.Implementation
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepo StatusRepo;
        private readonly IMapper mapper;

        public StatusService(IStatusRepo StatusRepo, IMapper mapper)
        {
            this.StatusRepo = StatusRepo;
            this.mapper = mapper;
        }

        public List<StatusDTO> GetAll()
        {
            return StatusRepo.GetAll().Select(a => mapper.Map<StatusDTO>(a)).ToList();
        }
    }
}
