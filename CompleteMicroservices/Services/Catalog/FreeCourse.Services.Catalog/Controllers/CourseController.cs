using FreeCourse.Services.Catalog.Dtos.CourseDtos;
using FreeCourse.Services.Catalog.Services.Abstract;
using FreeCourse.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CourseController : CustomBaseController
    {
        private readonly ICourseService _course;

        public CourseController(ICourseService course)
        {
            _course = course;
        }
        [HttpGet] 
        public async Task<IActionResult> List()
        {
            var data = await _course.GetAllAsync();
            return CreateActionResultInstance(data);
        } 
         
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await _course.GetByIdAsync(id);
            return CreateActionResultInstance(data);
        }
          
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var data = await _course.GetAllByUserIdAsync(userId);
            return CreateActionResultInstance(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDto model)
        {
            var data = await _course.CreateAsync(model);
            return CreateActionResultInstance(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCourseDto model)
        {
            await _course.UpdateAsync(model);
            return CreateActionResultInstance(new Shared.Dtos.ResponseObject<string>(model.Id, 200, true));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            await _course.DeleteByIdAsync(id);
            return CreateActionResultInstance(new Shared.Dtos.ResponseObject<string>(id, 200, true)); 
        }
    }
}
