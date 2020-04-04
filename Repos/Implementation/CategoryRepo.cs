using System.Collections.Generic;
using System.Linq;
using Context;
using Microsoft.EntityFrameworkCore;
using Models.DbModels;
using Repos.Contracts;

namespace Repos.Implementation
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly MainDbContext mainDbContext;

        public CategoryRepo(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }
        public List<Category> GetAll()
        {
            return mainDbContext.Categories.AsNoTracking().ToList();
        }
    }
}
