using System.Text.Json.Nodes;
using MiniApp.CRUD.Jsons;

namespace MiniApp.Tests.CRUD.Jsons.Unit
{
    public class JsonDataTests
    {
        [Fact]
        public void AddNewJsonTest()
        {
            // Arrange
            JsonData jsonData = new();
            JsonObject newObj = new()
            {
                ["id"] = 1,
                ["name"] = "Franco"
            };

            // Act
            jsonData.Add(newObj);
            JsonArray all = jsonData.GetAll();

            // Assert
            Assert.Single(all);
            Assert.Equal("Franco", all[0]?["name"]?.ToString());
        }

        [Fact]
        public void SearchByIdJsonTest()
        {
            // Arrange
            JsonData jsonData = new();
            JsonObject obj = new()
            {
                ["id"] = 5,
                ["name"] = "Test"
            };
            jsonData.Add(obj);

            // Act
            JsonObject? found = jsonData.SearchById(5);

            // Assert
            Assert.NotNull(found);
            Assert.Equal("Test", found?["name"]?.ToString());
        }

        [Fact]
        public void UpdateByIdJsonTest()
        {
            // Arrange
            JsonData jsonData = new();
            jsonData.Add(new JsonObject
            {
                ["id"] = 10,
                ["name"] = "OldName",
                ["age"] = 25
            });

            JsonObject updated = new()
            {
                ["name"] = "NewName",
                ["age"] = 26
            };

            // Act
            bool result = jsonData.UpdateById(10, updated);
            JsonObject? found = jsonData.SearchById(10);

            // Assert
            Assert.True(result);
            Assert.NotNull(found);
            Assert.Equal("NewName", found?["name"]?.ToString());
            Assert.Equal("26", found?["age"]?.ToString());
        }

        [Fact]
        public void DeleteByIdJsonTest()
        {
            // Arrange
            JsonData jsonData = new();
            jsonData.Add(new JsonObject
            {
                ["id"] = 20,
                ["name"] = "ToDelete"
            });

            // Act
            bool deleted = jsonData.DeleteById(20);
            JsonObject? found = jsonData.SearchById(20);

            // Assert
            Assert.True(deleted);
            Assert.Null(found);
            Assert.Empty(jsonData.GetAll());
        }

        [Fact]
        public void SearchById_NonExistent_ReturnsNull()
        {
            JsonData jsonData = new();

            // Act
            JsonObject? result = jsonData.SearchById(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeleteById_NonExistent_ReturnsFalse()
        {
            JsonData jsonData = new();

            // Act
            bool deleted = jsonData.DeleteById(42);

            // Assert
            Assert.False(deleted);
        }

        [Fact]
        public void Add_DuplicateId_ThrowsException()
        {
            JsonData jsonData = new();
            jsonData.Add(new JsonObject { ["id"] = 1, ["name"] = "A" });

            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                jsonData.Add(new JsonObject { ["id"] = 1, ["name"] = "Duplicate" });
            });
        }
    }
}
