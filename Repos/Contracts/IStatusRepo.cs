using System.Collections.Generic;
using Models.DbModels;

namespace Repos.Contracts
{
    public interface IStatusRepo
    {
        List<Status> GetAll();
    }
}
