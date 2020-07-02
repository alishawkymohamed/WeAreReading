using Models.DbModels;
using Models.DTOs;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IStatusService : IService<Status>
    {
        List<StatusDTO> GetAll();
    }
}
