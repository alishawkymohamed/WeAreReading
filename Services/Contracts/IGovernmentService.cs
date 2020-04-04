using System.Collections.Generic;
using Models.DbModels;
using Models.DTOs;

namespace Services.Contracts
{
    public interface IGovernmentService : IService<Government>
    {
        List<GovernmentDTO> GetAll();
    }
}
