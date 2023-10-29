﻿using FreeCourse.Services.Catalog.Dtos.FeatureDtos;
using FreeCourse.Services.Catalog.Models;

namespace FreeCourse.Services.Catalog.Dtos.CourseDtos
{
    public class CreateCourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; } 
        public FeatureDto Feature { get; set; }
        public string CategoryId { get; set; } 
    }
}
