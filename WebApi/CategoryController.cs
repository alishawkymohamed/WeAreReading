using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using Models.DTOs;
using Services.Contracts;

namespace WebApi
{
    public class CategoryController : BaseController<Category>
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService) : base(categoryService)
        {
            this.categoryService = categoryService;
        }


        [HttpGet("GetAll")]
        [ProducesResponseType(200, Type = typeof(List<CategoryDTO>))]
        public IActionResult GetAll()
        {
            return Ok(categoryService.GetAll());
        }
    }
}
