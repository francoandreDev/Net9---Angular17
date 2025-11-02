using System.Text.Json.Nodes;
using MiniApp.CRUD.Jsons;

namespace MiniApp.Tests.CRUD.Jsons.Integrations
{
    public class JsonDataIntegrationTests
    {
        [Fact]
        public void FullCrudFlow_ShouldBehaveConsistently()
        {
            // Arrange
            JsonData jsonData = new();

            // CREATE
            JsonObject obj = new()
            {
                ["id"] = 100,
                ["name"] = "Original"
            };
            jsonData.Add(obj);

            // READ
            JsonObject? found = jsonData.SearchById(100);
            Assert.NotNull(found);
            Assert.Equal("Original", found?["name"]?.ToString());

            // UPDATE
            JsonObject updated = new()
            {
                ["name"] = "Updated"
            };
            bool updatedOk = jsonData.UpdateById(100, updated);
            Assert.True(updatedOk);

            // VERIFY UPDATE
            JsonObject? afterUpdate = jsonData.SearchById(100);
            Assert.NotNull(afterUpdate);
            Assert.Equal("Updated", afterUpdate?["name"]?.ToString());

            // DELETE
            bool deleted = jsonData.DeleteById(100);
            Assert.True(deleted);

            // VERIFY DELETE
            JsonObject? afterDelete = jsonData.SearchById(100);
            Assert.Null(afterDelete);
        }
    }

}