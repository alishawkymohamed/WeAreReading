using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Models.DbModels;

namespace Repos.Contracts
{
    public interface IBookRepo
    {
        List<Book> GetAll(string search = null, Expression<Func<Book, bool>> expression = null);
        List<Book> GetLastAddedBooks(int count);
        List<Book> GetRecommendedBooks(int count);
        Book Get(Expression<Func<Book, bool>> expression);
        Book Insert(Book book);
        Book Update(Book book);
        void Delete(IEnumerable<Book> books);
    }
}
