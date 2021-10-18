using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoDB.Bson;
using api.Models;
using System;

namespace apiTest
{
    [TestClass]
    public class TestDatabase
    {   

        static private string collectionName = "inventory";
        static private string connectionString = "mongodb://root:secret@localhost:27017";
        static private string databaseName = "test";

        static private MongoClient client = new MongoClient(connectionString);
        static private IMongoDatabase database = client.GetDatabase(databaseName);

        IMongoCollection<Test> collection = database.GetCollection<Test>(collectionName);

        [TestMethod]
        public void TestDbConnectionSettings()
        {
            var isMongoLive = database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
            
            Assert.IsTrue(isMongoLive);
        }
    
        [TestMethod]
        public void TestDbInsertDocument()
        {
            Test testdata = new Test()
            {
                item = "item",
                qty = 1,
                tags = "tags",
                size = "size 1",
            };

            try 
            {
                collection.InsertOne(testdata);
                Assert.IsTrue(true);
            } catch (Exception e) {
                Assert.IsNull(e);
            }
        }

        [TestMethod]
        public void TestDbQueryDocument()
        {
            var filter = Builders<Test>.Filter.Eq("item", "item");
            var result = collection.Find(filter).ToList();

            Assert.IsTrue(result[0].size == "size 1");
        }

        [TestMethod]
        public void TestDbUpdateDocument()
        {
            var filter = Builders<Test>.Filter.Eq("item", "item");
            var update = Builders<Test>.Update.Set("item", "itemUpdated");
            var result = collection.UpdateOne(filter, update);
            
            Assert.IsTrue(result.ModifiedCount > 0);
        }

        [TestMethod]
        public void TestDbDeleteIDocument()
        {
            var filter = Builders<Test>.Filter.Eq("item", "itemUpdated");
            var result = collection.DeleteMany(filter);
            
            Assert.IsTrue(result.DeletedCount > 0);
        }
    }
}
