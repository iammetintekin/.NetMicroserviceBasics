using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Utilities.AppSettingsConfig;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    class CollectionManager : ICollectionManager
    {
        public static IDatabaseConfig _dbConfig;
        public CollectionManager(IDatabaseConfig databaseConfig)
        {
            _dbConfig = databaseConfig; 

            var client = new MongoClient(databaseConfig.ConnectionString);
            var DB = client.GetDatabase(databaseConfig.Name);

            _categoryCollection = new Lazy<IMongoCollection<Category>>(()=> DB.GetCollection<Category>(_dbConfig.Collection.Category));
            _courseCollection = new Lazy<IMongoCollection<Course>>(() => DB.GetCollection<Course>(_dbConfig.Collection.Course));
        }
        private Lazy<IMongoCollection<Category>> _categoryCollection;
        private Lazy<IMongoCollection<Course>> _courseCollection;

        public IMongoCollection<Category> CategoryCollection => _categoryCollection.Value;
        public IMongoCollection<Course> CourseCollection => _courseCollection.Value;
    }
}
