using Models.DbModels;

namespace Repos.Contracts
{
    public interface IUserRoleRepo
    {
        UserRole Insert(UserRole userRole);
    }
}
