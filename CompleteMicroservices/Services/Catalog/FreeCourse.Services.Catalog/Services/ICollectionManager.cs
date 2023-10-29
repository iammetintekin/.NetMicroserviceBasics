﻿using FreeCourse.Services.Catalog.Models;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICollectionManager
    {
       public IMongoCollection<Category> CategoryCollection { get; set; } 
       public IMongoCollection<Course> CourseCollection { get; set; } 
    }
}
