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
        IMongoDatabase database = client.GetDatabase(databaseName);

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

            var data = database.GetCollection<Test>("inventory");
            try 
            {
                data.InsertOne(testdata);
                Assert.IsTrue(true);
            } catch (Exception e) {
                Assert.IsNull(e);
            }
        }

        [TestMethod]
        public void TestDbGetFromItem()
        {
            var collection = database.GetCollection<Test>(collectionName);

            var filter = Builders<Test>.Filter.Eq("item", "item");
            var result = collection.Find(filter).ToList();

            Assert.IsTrue(result[0].size == "size 1");
        }

        [TestMethod]
        public void TestDbRemoveItem()
        {
            var collection = database.GetCollection<Test>(collectionName);
            var filter = Builders<Test>.Filter.Eq("item", "item");

            var result = collection.DeleteMany(filter);
            Assert.IsTrue(result.DeletedCount > 0);
        }
    }
}
