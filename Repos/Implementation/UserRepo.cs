using System;
using System.Collections.Generic;
using System.Text;
using Context;
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
            this.mainDbContext.Add(user);
            this.mainDbContext.SaveChanges();
            return user;
        }
    }
}
