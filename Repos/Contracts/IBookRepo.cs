using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Models.DbModels;

namespace Repos.Contracts
{
    public interface IBookRepo
    {
        List<Book> GetAll(Expression<Func<Book, bool>> expression);
        Book Get(Expression<Func<Book, bool>> expression);
        Book Insert(Book book);
        Book Update(Book book);
        void Delete(IEnumerable<Book> books);
    }
}
