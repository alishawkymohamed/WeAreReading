using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Models.DTOs;
using Repos.Contracts;
using Services.Contracts;

namespace Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo categoryRepo;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepo categoryRepo, IMapper mapper)
        {
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
        }



        public List<CategoryDTO> GetAll()
        {
            return categoryRepo.GetAll().Select(a => mapper.Map<CategoryDTO>(a)).ToList();
        }
    }
}
