using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Models.DbModels;

namespace Repos.Contracts
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAll(Expression<Func<User, bool>> expression);
        User Get(Expression<Func<User, bool>> expression);
        User Insert(User user);
    }
}
