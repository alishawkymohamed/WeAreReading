using System.Collections.Generic;
using System.Linq;
using Context;
using Microsoft.EntityFrameworkCore;
using Models.DbModels;
using Repos.Contracts;

namespace Repos.Implementation
{
    public class GovernmentRepo : IGovernmentRepo
    {
        private readonly MainDbContext mainDbContext;

        public GovernmentRepo(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }
        public List<Government> GetAll()
        {
            return mainDbContext.Governments.AsNoTracking().ToList();
        }
    }
}
