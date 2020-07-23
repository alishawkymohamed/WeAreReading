using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Models.DbModels;

namespace Repos.Contracts
{
    public interface IRequestRepo
    {
        List<Request> GetAll(Expression<Func<Request, bool>> expression = null);
        Request Get(Expression<Func<Request, bool>> expression);
        Request Insert(Request Request);
        Request Update(Request Request);
        void Delete(IEnumerable<Request> Requests);
    }
}
