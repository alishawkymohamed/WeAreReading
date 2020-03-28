using Context;
using Models.DbModels;
using Repos.Contracts;

namespace Repos.Implementation
{
    public class UserRoleRepo : IUserRoleRepo
    {
        private readonly MainDbContext mainDbContext;

        public UserRoleRepo(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }
        public UserRole Insert(UserRole userRole)
        {
            mainDbContext.UserRoles.Add(userRole);
            mainDbContext.SaveChanges();
            return userRole;
        }
    }
}
