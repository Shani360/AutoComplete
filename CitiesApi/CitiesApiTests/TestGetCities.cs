using CitiesApi;
using CitiesApi.Controllers;
using CitiesApi.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CitiesApiTests
{
    [TestClass]
    public class TestGetCities
    {
        [TestMethod]
        public async Task TestMethod1Async()
        {
            // Arrange
            var mockContext = new Mock<DataContext>("Los");
            var cities = new List<City> { new City { Name = "New York" }, new City { Name = "Los Angeles" } };

            // Set up the mock to return the list of cities when GetCities is called
            mockContext.Setup(c => c.GetCities()).Returns(cities.AsQueryable());

            var controller = new CitiesController(mockContext.Object);

            // Act
            var result = await controller.GetSearchCities("Los");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<City>>));

            var okResult = result as ActionResult<List<City>>;
            Assert.IsNotNull(okResult);

            var value = okResult.Value as List<City>;
            CollectionAssert.AreEqual(cities, value);
        }
    }
}