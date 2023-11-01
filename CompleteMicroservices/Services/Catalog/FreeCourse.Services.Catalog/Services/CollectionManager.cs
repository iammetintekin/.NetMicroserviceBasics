using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Utilities.AppSettingsConfig;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    class CollectionManager : ICollectionManager
    {
        public static IDatabaseConfig _dbConfig;
        public static IMongoDatabase _database;
        public CollectionManager(IDatabaseConfig databaseConfig, IMongoDatabase mongoDatabase)
        {
            _dbConfig = databaseConfig; 
            var client = new MongoClient(databaseConfig.ConnectionString);
            // Connects catalog db
            _database = client.GetDatabase(databaseConfig.Name); 
        } 
        public IMongoCollection<Category> CategoryCollection { get; set; } = _database.GetCollection<Category>(_dbConfig.Collection.Category);
        public IMongoCollection<Course> CourseCollection { get; set; } = _database.GetCollection<Course>(_dbConfig.Collection.Course);
    }
}
