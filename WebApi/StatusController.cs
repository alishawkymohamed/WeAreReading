using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using Models.DTOs;
using Services.Contracts;
using System.Collections.Generic;

namespace WebApi
{
    public class StatusController : BaseController<Status>
    {
        private readonly IStatusService StatusService;

        public StatusController(IStatusService StatusService) : base(StatusService)
        {
            this.StatusService = StatusService;
        }


        [HttpGet("GetAll")]
        [ProducesResponseType(200, Type = typeof(List<StatusDTO>))]
        public IActionResult GetAll()
        {
            return Ok(StatusService.GetAll());
        }
    }
}
