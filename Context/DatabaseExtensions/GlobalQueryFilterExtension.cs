using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models.DbModels.TrackingInterfaces;

namespace Context.DatabaseExtensions
{
    public static class GlobalQueryFilterExtension
    {
        public static void SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
        {
            SetSoftDeleteFilterMethod.MakeGenericMethod(entityType)
                .Invoke(null, new object[] { modelBuilder });
        }

        private static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(GlobalQueryFilterExtension)
           .GetMethods(BindingFlags.Public | BindingFlags.Static)
           .Single(t => t.IsGenericMethod && t.Name == "SetSoftDeleteFilter");

        public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
            where TEntity : class, IAuditableDelete
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
