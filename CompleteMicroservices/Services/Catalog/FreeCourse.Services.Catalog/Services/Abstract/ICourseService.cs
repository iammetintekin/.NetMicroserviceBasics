using FreeCourse.Services.Catalog.Dtos.CourseDtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services.Abstract
{
    public interface ICourseService
    {
        Task<ResponseObject<List<CourseDto>>> GetAllAsync();
        Task<ResponseObject<CourseDto>> CreateAsync(CreateCourseDto CourseDto);
        Task<ResponseObject<CourseDto>> GetByIdAsync(string Id);
        Task<ResponseObject<List<CourseDto>>> GetAllByUserIdAsync(string UserId);
        Task UpdateAsync(UpdateCourseDto CourseDto);
        Task DeleteByIdAsync(string Id);
    }
}
