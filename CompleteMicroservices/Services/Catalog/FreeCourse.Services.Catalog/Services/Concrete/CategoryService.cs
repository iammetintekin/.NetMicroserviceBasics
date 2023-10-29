using AutoMapper;
using FreeCourse.Services.Catalog.Dtos.CategoryDtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Services.Abstract;
using FreeCourse.Services.Catalog.Utilities.AppSettingsConfig;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FreeCourse.Services.Catalog.Services.Concrete
{
    class CategoryService: ICategoryService
    {
        private readonly ICollectionManager _collection;
        private readonly IMapper _mapper;
        public CategoryService(ICollectionManager collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;            
        }
        public async Task<ResponseObject<List<CategoryDto>>> GetAllAsync()
        {
            var data = await _collection.CategoryCollection.Find(ctaegory => true).ToListAsync();
            return new ResponseObject<List<CategoryDto>>(_mapper.Map<List<CategoryDto>>(data), 200, true);
        }
        public async Task<ResponseObject<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            await _collection.CategoryCollection.InsertOneAsync(_mapper.Map<Category>(categoryDto));
            return new ResponseObject<CategoryDto>(categoryDto, 200,true);
        }
        public async Task<ResponseObject<CategoryDto>> GetByIdAsync(string Id)
        {
            var category = await _collection.CategoryCollection.Find(s=>s.Id == Id).SingleOrDefaultAsync();
            if (category is null)
                return new ResponseObject<CategoryDto>(null, 404, false, new List<string> { "Data not found" });
            return new ResponseObject<CategoryDto>(_mapper.Map<CategoryDto>(category), 200, true);
        }
    }
}
