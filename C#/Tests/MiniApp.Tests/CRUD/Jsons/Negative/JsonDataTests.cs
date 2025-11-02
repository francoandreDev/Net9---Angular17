using System.Text.Json.Nodes;
using MiniApp.CRUD.Jsons;

namespace MiniApp.Tests.CRUD.Jsons.Negative
{
    public class JsonDataNegativeTests
    {
        [Fact]
        public void UpdateById_ShouldReturnFalse_WhenIdDoesNotExist()
        {
            // Arrange
            JsonData jsonData = new();
            JsonObject obj = new()
            {
                ["id"] = 1,
                ["name"] = "John"
            };
            jsonData.Add(obj);

            JsonObject newData = new()
            {
                ["name"] = "Updated"
            };

            // Act
            bool result = jsonData.UpdateById(99, newData);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UpdateById_ShouldReturnFalse_WhenNewDataIsNull()
        {
            // Arrange
            JsonData jsonData = new();
            JsonObject obj = new()
            {
                ["id"] = 10,
                ["name"] = "Anna"
            };
            jsonData.Add(obj);

            // Act
            bool result = jsonData.UpdateById(10, null!);

            // Assert
            Assert.False(result);
        }
    }

}