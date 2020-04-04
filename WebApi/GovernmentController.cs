using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using Models.DTOs;
using Services.Contracts;

namespace WebApi
{
    public class GovernmentController : BaseController<Government>
    {
        private readonly IGovernmentService governmentService;

        public GovernmentController(IGovernmentService governmentService) : base(governmentService)
        {
            this.governmentService = governmentService;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(200, Type = typeof(List<GovernmentDTO>))]
        public IActionResult GetAll()
        {
            return Ok(governmentService.GetAll());
        }
    }
}
