using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models.DbModels;
using Repos.Contracts;

namespace Repos.Implementation
{
    public class BookRepo : IBookRepo
    {
        private readonly MainDbContext mainDbContext;

        public BookRepo(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public Book Get(Expression<Func<Book, bool>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            return this.mainDbContext.Books
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x => x.Category)
                    .FirstOrDefault(expression);
        }

        public List<Book> GetAll(Expression<Func<Book, bool>> expression = null)
        {
            if (expression != null)
            {
                return this.mainDbContext.Books
                                    .Include(x => x.User)
                                    .Include(x => x.Category)
                                    .Where(expression)
                                    .AsNoTracking().ToList();
            }
            else
            {
                return this.mainDbContext.Books
                                    .Include(x => x.User)
                                    .Include(x => x.Category)
                                    .AsNoTracking().ToList();
            }
        }

        public Book Insert(Book book)
        {
            mainDbContext.Books.Add(book);
            mainDbContext.SaveChanges();
            return book;
        }

        public Book Update(Book book)
        {
            EntityEntry<Book> DbEntry = this.mainDbContext.Attach(this.mainDbContext.Set<Book>().FirstOrDefault(x => x.Id == book.Id));
            DbEntry.CurrentValues.SetValues(book);
            this.mainDbContext.SaveChanges();
            return book;
        }

        public void Delete(IEnumerable<Book> books)
        {
            foreach (Book book in books)
            {
                book.IsDeleted = true;
                book.DeletedAt = DateTime.Now;
                this.Update(book);
            }
            this.mainDbContext.SaveChanges();
        }
    }
}
