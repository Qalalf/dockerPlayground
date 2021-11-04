using System;
using System.Collections.Generic;
using api.Models;
using MongoDB.Driver;

namespace api.Services
{
    public class HabitItemService
    {
        private readonly IMongoCollection<HabitItem> _habitCollection;

        public HabitItemService(IMongoDatabaseSettings mongoDatabaseSettings)
        {
            var client = new MongoClient(mongoDatabaseSettings.ConnectionString);
            var database = client.GetDatabase(mongoDatabaseSettings.DatabaseName);
            _habitCollection = database.GetCollection<HabitItem>(mongoDatabaseSettings.CollectionName);
        }

        public List<HabitItem> GetAll() =>
            _habitCollection.Find(a => true).ToList();

        public HabitItem Get(string id) =>
            _habitCollection.Find<HabitItem>(item => item.Id.Equals(id)).FirstOrDefault();

        public bool Insert(HabitItem habitItem)
        {
            try
            {
                _habitCollection.InsertOne(habitItem);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Update(string id, HabitItem habitItem)
        {
            var filter = Builders<HabitItem>.Filter.Eq(item => item.Id, id);
            var update = Builders<HabitItem>.Update
                .Set(item => item.Anchor, habitItem.Anchor)
                .Set(item => item.Microhabit, habitItem.Microhabit)
                .Set(item => item.Celebration, habitItem.Celebration);
            
            try
            {
                _habitCollection.UpdateOne(filter, update);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Delete(string id)
        {
            var filter = Builders<HabitItem>.Filter.Eq(item => item.Id, id);
            try
            {
                _habitCollection.DeleteOne(filter);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
        }
            
    }
}