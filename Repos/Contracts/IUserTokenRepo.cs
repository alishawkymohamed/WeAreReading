using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Models.DbModels;

namespace Repos.Contracts
{
    public interface IUserTokenRepo
    {
        IEnumerable<UserToken> GetAll(Expression<Func<UserToken, bool>> expression);
        UserToken Get(Expression<Func<UserToken, bool>> expression);
        UserToken Insert(UserToken userToken);
        void Delete(IEnumerable<UserToken> userTokens);
    }
}
