using Models.DbModels;

namespace Repos.Contracts
{
    public interface IUserRepo
    {
        User Insert(User user);
    }
}
