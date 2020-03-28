using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using Services.Contracts;

namespace WebApi
{
    public class RoleController : BaseController<Role>
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService) : base(roleService)
        {
            this.roleService = roleService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(this.roleService.GetAll());
        }
    }
}
