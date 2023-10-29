using AutoMapper;
using FreeCourse.Services.Catalog.Dtos.CourseDtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Services.Abstract;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FreeCourse.Services.Catalog.Services.Concrete
{
    public class CourseService: ICourseService
    {
        private readonly ICollectionManager _collection;
        private readonly IMapper _mapper;
        public CourseService(ICollectionManager collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }
        public async Task<ResponseObject<List<CourseDto>>> GetAllAsync()
        {
            var data = await _collection.CourseCollection.Find(course => true).ToListAsync();
            if (data.Any())
            {
                foreach (var item in data)
                {
                    item.Category = await _collection.CategoryCollection.Find(s => s.Id == item.CategoryId).SingleOrDefaultAsync();
                }
            }
            else
            {
                data = new List<Course>();
            }

            return new ResponseObject<List<CourseDto>>(_mapper.Map<List<CourseDto>>(data), 200, true);
        }
        public async Task<ResponseObject<CourseDto>> CreateAsync(CreateCourseDto CourseDto)
        {
            var preparedCourse = _mapper.Map<Course>(CourseDto);
            preparedCourse.CreatedTime = DateTime.Now;
            await _collection.CourseCollection.InsertOneAsync(preparedCourse);
            return new ResponseObject<CourseDto>(_mapper.Map<CourseDto>(CourseDto), 200, true);
        }
        public async Task<ResponseObject<CourseDto>> GetByIdAsync(string Id)
        {
            var Course = await _collection.CourseCollection.Find(s => s.Id == Id).SingleOrDefaultAsync();
            if (Course is null)
                return new ResponseObject<CourseDto>(null, 404, false, new List<string> { "Data not found" });
            return new ResponseObject<CourseDto>(_mapper.Map<CourseDto>(Course), 200, true);
        }
        public async Task<ResponseObject<List<CourseDto>>> GetAllByUserIdAsync(string UserId)
        {
            var Courses = await _collection.CourseCollection.Find(s => s.UserId == UserId).ToListAsync();

            if (!Courses.Any())
                Courses = new List<Course>();

            return new ResponseObject<List<CourseDto>>(_mapper.Map<List<CourseDto>>(Courses), 200, true);
        }
        public async Task UpdateAsync(UpdateCourseDto CourseDto)
        {
            var preparedCourse = _mapper.Map<Course>(CourseDto);
            var data = await _collection.CourseCollection.Find(s => s.Id == CourseDto.Id).SingleOrDefaultAsync(); 
            if (data is not null)
                await _collection.CourseCollection.FindOneAndReplaceAsync(s => s.Id == CourseDto.Id, preparedCourse);
        }
        public async Task DeleteByIdAsync(string Id)
        {
            var Course = await _collection.CourseCollection.Find(s => s.Id == Id).SingleOrDefaultAsync();
            if (Course is not null)
                await _collection.CourseCollection.DeleteOneAsync(s => s.Id == Id);
        }
    }
}
