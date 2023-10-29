using FreeCourse.Services.Catalog.Dtos.CategoryDtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services.Abstract
{
    public interface ICategoryService
    {
        Task<ResponseObject<List<CategoryDto>>> GetAllAsync();
        Task<ResponseObject<CategoryDto>> CreateAsync(CategoryDto category);
        Task<ResponseObject<CategoryDto>> GetByIdAsync(string Id);
    }
}
