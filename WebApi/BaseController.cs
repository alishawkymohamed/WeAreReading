using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using Services.Contracts;

namespace WebApi
{
    [Route("api/[controller]")]
    public class BaseController<TDbEntity> : Controller where TDbEntity : BaseEntity
    {
        private readonly IService<TDbEntity> service;

        public BaseController(IService<TDbEntity> service)
        {
            this.service = service;
        }
    }
}
