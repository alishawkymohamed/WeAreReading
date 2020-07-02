using Context;
using Microsoft.EntityFrameworkCore;
using Models.DbModels;
using Repos.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Repos.Implementation
{
    public class StatusRepo : IStatusRepo
    {
        private readonly MainDbContext mainDbContext;

        public StatusRepo(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }
        public List<Status> GetAll()
        {
            return mainDbContext.Statuses.AsNoTracking().ToList();
        }
    }
}
