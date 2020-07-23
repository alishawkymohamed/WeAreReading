using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Context;
using Helpers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models.DbModels;
using Repos.Contracts;

namespace Repos.Implementation
{
    public class BookRepo : IBookRepo
    {
        private readonly MainDbContext mainDbContext;
        private readonly ISessionService sessionService;

        public BookRepo(MainDbContext mainDbContext, ISessionService sessionService)
        {
            this.mainDbContext = mainDbContext;
            this.sessionService = sessionService;
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

        public List<Book> GetAll(string search = null, Expression<Func<Book, bool>> expression = null)
        {
            IQueryable<Book> result = null;
            if (expression != null)
            {
                result = this.mainDbContext.Books
                                    .Include(x => x.User)
                                    .Include(x => x.Category)
                                    .Include(x => x.Status)
                                    .Where(expression)
                                    .AsNoTracking();
            }
            else
            {
                result = this.mainDbContext.Books
                                    .Include(x => x.User)
                                    .Include(x => x.Category)
                                    .Include(x => x.Status)
                                    .AsNoTracking();
            }

            if (search != null && search.Length > 0)
            {
                return result.Where(x => EF.Functions.Like(x.Title, $"%{search}%") || EF.Functions.Like(x.Author, $"%{search}%")).ToList();
            }
            else
            {
                return result.ToList();
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

        public List<Book> GetLastAddedBooks(int count)
        {
            return this.mainDbContext.Books
                                    .Include(x => x.User)
                                    .Include(x => x.Category)
                                    .Include(x => x.Status)
                                    .AsNoTracking()
                                    .OrderByDescending(x => x.Id)
                                    .Take(count).ToList();
        }

        public List<Book> GetRecommendedBooks(int count)
        {
            var userId = sessionService.UserId;
            return this.mainDbContext.Books
                                    .Include(x => x.User)
                                    .Include(x => x.Category)
                                    .Include(x => x.Status)
                                    .Where(x => x.OwnerId != userId)
                                    .AsNoTracking()
                                    .OrderByDescending(x => x.Rating)
                                    .Take(count).ToList();
        }
    }
}
