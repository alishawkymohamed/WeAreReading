using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Context;
using Microsoft.EntityFrameworkCore;
using Models.DbModels;
using Repos.Contracts;

namespace Repos.Implementation
{
    public class UserRepo : IUserRepo
    {
        private readonly MainDbContext mainDbContext;

        public UserRepo(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public User Insert(User user)
        {
            mainDbContext.Add(user);
            mainDbContext.SaveChanges();
            return user;
        }

        public User Get(Expression<Func<User, bool>> expression)
        {
            return mainDbContext.Users
                .Include(x => x.UserRoles)
                .Include(x => x.Government)
                .AsNoTracking()
                .FirstOrDefault(expression);
        }

        public IEnumerable<User> GetAll(Expression<Func<User, bool>> expression)
        {
            return mainDbContext.Users
                .Include(x => x.UserRoles)
                .Include(x => x.Government)
                .Where(expression).ToList();
        }
    }
}
