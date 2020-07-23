using Context;
using Helpers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models.DbModels;
using Repos.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repos.Implementation
{
    public class RequestRepo : IRequestRepo
    {
        private readonly MainDbContext mainDbContext;

        public RequestRepo(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public Request Get(Expression<Func<Request, bool>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            return this.mainDbContext.Requests
                    .AsNoTracking()
                    .Include(x => x.Sender)
                    .Include(x => x.Receiver)
                    .Include(x => x.Book)
                    .FirstOrDefault(expression);
        }

        public List<Request> GetAll(Expression<Func<Request, bool>> expression = null)
        {
            IQueryable<Request> result = null;
            if (expression != null)
            {
                result = this.mainDbContext.Requests
                                            .Include(x => x.Sender)
                                            .Include(x => x.Receiver)
                                            .Include(x => x.Book)
                                            .Where(expression)
                                            .AsNoTracking();
            }
            else
            {
                result = this.mainDbContext.Requests
                                            .Include(x => x.Sender)
                                            .Include(x => x.Receiver)
                                            .Include(x => x.Book)
                                            .AsNoTracking();
            }

            return result.ToList();
        }

        public Request Insert(Request Request)
        {
            mainDbContext.Requests.Add(Request);
            mainDbContext.SaveChanges();
            return Request;
        }

        public Request Update(Request Request)
        {
            EntityEntry<Request> DbEntry = this.mainDbContext.Attach(this.mainDbContext.Set<Request>().FirstOrDefault(x => x.Id == Request.Id));
            DbEntry.CurrentValues.SetValues(Request);
            this.mainDbContext.SaveChanges();
            return Request;
        }

        public void Delete(IEnumerable<Request> Requests)
        {
            foreach (Request Request in Requests)
            {
                Request.IsDeleted = true;
                Request.DeletedAt = DateTime.Now;
                this.Update(Request);
            }
            this.mainDbContext.SaveChanges();
        }
    }
}
