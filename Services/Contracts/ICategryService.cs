using System.Collections.Generic;
using Models.DbModels;
using Models.DTOs;

namespace Services.Contracts
{
    public interface ICategoryService : IService<Category>
    {
        List<CategoryDTO> GetAll();
    }
}
