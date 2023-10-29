using AutoMapper;
using FreeCourse.Services.Catalog.Dtos.CategoryDtos;
using FreeCourse.Services.Catalog.Dtos.CourseDtos;
using FreeCourse.Services.Catalog.Dtos.FeatureDtos;
using FreeCourse.Services.Catalog.Models;

namespace FreeCourse.Services.Catalog.Utilities.AutoMapper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        { 
            CreateMap<Course, CourseDto>().ReverseMap(); 
            CreateMap<Category, CategoryDto>().ReverseMap(); 
            CreateMap<Feature, FeatureDto>().ReverseMap(); 

            CreateMap<Course, CreateCourseDto>().ReverseMap(); 
            CreateMap<Course, UpdateCourseDto>().ReverseMap(); 
            CreateMap<CreateCourseDto, CourseDto>().ReverseMap(); 
        }
    }
}
