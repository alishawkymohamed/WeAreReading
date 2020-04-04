using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using Models.DTOs;
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
        [ProducesResponseType(200, Type = typeof(List<RoleDTO>))]
        public IActionResult GetAll()
        {
            return Ok(this.roleService.GetAll());
        }
    }
}
