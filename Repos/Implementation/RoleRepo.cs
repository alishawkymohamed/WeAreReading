using Context;
using Microsoft.EntityFrameworkCore;
using Models.DbModels;
using Repos.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repos.Implementation
{
    public class RoleRepo : IRoleRepo
    {
        private readonly MainDbContext mainDbContext;

        public RoleRepo(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }
        public List<Role> GetAll()
        {
            return this.mainDbContext.Roles.AsNoTracking().ToList();
        }
    }
}
