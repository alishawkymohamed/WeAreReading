using System.Collections.Generic;
using Models.DbModels;

namespace Repos.Contracts
{
    public interface IGovernmentRepo
    {
        List<Government> GetAll();
    }
}
