using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Utilities.AppSettingsConfig;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    class CollectionManager : ICollectionManager
    {
        public readonly IDatabaseConfig _db;
        public CollectionManager(IDatabaseConfig databaseConfig)
        {
            _db = databaseConfig;
            var client = new MongoClient(databaseConfig.ConnectionString);
            var db = client.GetDatabase(databaseConfig.Name); 
            CategoryCollection =  db.GetCollection<Category>(databaseConfig.Collection.Category);
            CourseCollection = db.GetCollection<Course>(databaseConfig.Collection.Course);
        } 
        public IMongoCollection<Category> CategoryCollection { get; set; }
        public IMongoCollection<Course> CourseCollection { get; set; } 
    }
}
