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
    public class UserTokenRepo : IUserTokenRepo
    {
        private readonly MainDbContext mainDbContext;

        public UserTokenRepo(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public void Delete(IEnumerable<UserToken> userTokens)
        {
            mainDbContext.UserTokens.RemoveRange(userTokens);
            mainDbContext.SaveChanges();
        }

        public UserToken Get(Expression<Func<UserToken, bool>> expression)
        {
            return mainDbContext.UserTokens.Include(x => x.User).AsNoTracking().FirstOrDefault(expression);
        }

        public IEnumerable<UserToken> GetAll(Expression<Func<UserToken, bool>> expression)
        {
            return mainDbContext.UserTokens.AsNoTracking().Where(expression).ToList();
        }

        public UserToken Insert(UserToken userToken)
        {
            mainDbContext.Add(userToken);
            mainDbContext.SaveChanges();
            return userToken;
        }
    }
}
