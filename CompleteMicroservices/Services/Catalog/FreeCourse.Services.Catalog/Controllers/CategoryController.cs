using FreeCourse.Services.Catalog.Dtos.CategoryDtos;
using FreeCourse.Services.Catalog.Dtos.CourseDtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Services.Abstract;
using FreeCourse.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryService _category;

        public CategoryController(ICategoryService category)
        {
            _category = category;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var data = await _category.GetAllAsync();
            return CreateActionResultInstance(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await _category.GetByIdAsync(id);
            return CreateActionResultInstance(data);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto model)
        {
            var data = await _category.CreateAsync(model);
            return CreateActionResultInstance(data);
        }
    }
}
