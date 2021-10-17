using api.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace api.Services
{
    public class TestService
    {
        private readonly IMongoCollection<Test> _tests;

        public TestService(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _tests = database.GetCollection<Test>(settings.TestCollectionName);
        }

        public List<Test> Get() =>
            _tests.Find(Test => true).ToList();
        
        public Test Get(string id) =>
            _tests.Find<Test>(test => test.Id == id).FirstOrDefault();
    }
}