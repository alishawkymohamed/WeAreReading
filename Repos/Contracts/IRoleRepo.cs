using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repos.Contracts
{
    public interface IRoleRepo
    {
        List<Role> GetAll();
    }
}
