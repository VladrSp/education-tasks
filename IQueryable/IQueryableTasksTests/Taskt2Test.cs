using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IQueryableTasks.Task2;

namespace IQueryableTasksTests
{
    [TestClass]
    public class Taskt2Test
    {
        [TestMethod]
        public void MappingTest()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Food, Bar>();
            var food = new Food { Id = 1, Name = "TestData", BeerItem = new Beer { Id = 11, Name = "Duvel", Price = 11.1m } };
            var res = mapper.Map(food);

            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(Bar));
            Assert.AreEqual(food.Id, res.Id);
            Assert.AreEqual(food.Name, res.Name);

            Assert.IsNotNull(food.BeerItem);
            Assert.AreEqual(food.BeerItem.Id, res.BeerItem.Id);
            Assert.AreEqual(food.BeerItem.Price, res.BeerItem.Price);
            Assert.AreEqual(food.BeerItem.Name, res.BeerItem.Name);

        }

        [TestMethod]
        public void MappingIsNullStringTest()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Food, Bar>();
            var food = new Food { Id = 1, Name = null, BeerItem = new Beer { Id = 11, Name = "Duvel", Price = 11.1m } };
            var res = mapper.Map(food);

            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(Bar));
            Assert.AreEqual(food.Id, res.Id);
            Assert.AreEqual(food.Name, res.Name);

            Assert.IsNotNull(food.BeerItem);
            Assert.AreEqual(food.BeerItem.Id, res.BeerItem.Id);
            Assert.AreEqual(food.BeerItem.Price, res.BeerItem.Price);
            Assert.AreEqual(food.BeerItem.Name, res.BeerItem.Name);

        }

        [TestMethod]
        public void MappingIsNullBeerItemTest()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Food, Bar>();
            var food = new Food { Id = 1, Name = null};
            var res = mapper.Map(food);

            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(Bar));
            Assert.AreEqual(food.Id, res.Id);
            Assert.AreEqual(food.Name, res.Name);

            Assert.IsNull(food.BeerItem);
        }
    }
}
