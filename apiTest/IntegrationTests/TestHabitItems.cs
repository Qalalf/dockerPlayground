using System.Linq;
using api.Models;
using api.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace apiTest.IntegrationTests
{
    [TestClass]
    public class TestHabitItems
    {
        private IMongoDatabaseSettings _mockMongoDatabaseSettings = new MongoDatabaseSettings();
        private HabitItemService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMongoDatabaseSettings.CollectionName = "HabitTrackerTest";
            _mockMongoDatabaseSettings.ConnectionString = "mongodb://root:secret@localhost:27017";
            _mockMongoDatabaseSettings.DatabaseName = "test";
            _service = new HabitItemService(_mockMongoDatabaseSettings);
        }

        [TestMethod]
        public void TestInsert()
        {
            HabitItem newItem = new HabitItem()
            {
                Anchor = "WhenTesting",
                Microhabit = "DoTesting",
                Celebration = "CelebrateTesting"
            };

            var result = _service.Insert(newItem);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestGet()
        {
            var allData = _service.GetAll();
            var id = allData.FirstOrDefault().Id;

            var result = _service.Get(id);

            Assert.IsTrue(result.GetType() == typeof(HabitItem));
        }

        [TestMethod]
        public void TestGetAll()
        {
            var result = _service.GetAll();

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void TestUpdate()
        {
            HabitItem newData = new HabitItem()
            {
                Anchor = "updatedWhen",
                Microhabit = "updatedHow",
                Celebration = "updatedCelebration"
            };

            var habitItems = _service.GetAll();
            var itemToUpdate = habitItems.Find(habitItem => habitItem.Anchor.Equals("WhenTesting"));

            var result = _service.Update(itemToUpdate.Id, newData);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestDelete()
        {
            var habitItems = _service.GetAll();
            var itemToDelete = habitItems.Find(test => test.Anchor.Equals("updatedWhen"));

            var result = _service.Delete(itemToDelete.Id);

            Assert.IsTrue(result);
        }
    }
}