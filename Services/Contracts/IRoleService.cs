using Models.DbModels;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts
{
    public interface IRoleService :IService<Role>
    {
        List<RoleDTO> GetAll();
    }
}
