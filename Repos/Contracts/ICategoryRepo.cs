using System.Collections.Generic;
using Models.DbModels;

namespace Repos.Contracts
{
    public interface ICategoryRepo
    {
        List<Category> GetAll();
    }
}
