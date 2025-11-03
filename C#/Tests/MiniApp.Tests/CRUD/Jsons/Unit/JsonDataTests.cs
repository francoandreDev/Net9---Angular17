// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Unit tests for JsonData CRUD operations
// ***********************************************************************

using System.Text.Json.Nodes;
using MiniApp.CRUD.Jsons;

namespace MiniApp.Tests.CRUD.Jsons.Unit
{
    /// <summary>
    /// 🧩 Unit tests for <see cref="JsonData"/> operations.
    /// Ensures correct behavior for adding, searching, updating, and deleting JSON objects.
    /// </summary>
    public class JsonDataTests
    {
        #region ➕ Add Operations

        /// <summary>
        /// ✅ Verifies that adding a new JSON object correctly stores it in the collection.
        /// </summary>
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

        /// <summary>
        /// ❌ Verifies that attempting to add a duplicate ID throws an exception.
        /// </summary>
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

        #endregion

        #region 🔍 Search Operations

        /// <summary>
        /// 🔎 Tests searching for an existing object by ID.
        /// </summary>
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

        /// <summary>
        /// 🕳 Verifies that searching for a non-existent ID returns null.
        /// </summary>
        [Fact]
        public void SearchById_NonExistent_ReturnsNull()
        {
            JsonData jsonData = new();

            // Act
            JsonObject? result = jsonData.SearchById(999);

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region ✏️ Update Operations

        /// <summary>
        /// 🧾 Ensures that updating an existing JSON object modifies the correct fields.
        /// </summary>
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

        #endregion

        #region ❌ Delete Operations

        /// <summary>
        /// 🗑 Verifies that deleting an existing object by ID works correctly.
        /// </summary>
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

        /// <summary>
        /// 🧨 Ensures that attempting to delete a non-existent object returns false.
        /// </summary>
        [Fact]
        public void DeleteById_NonExistent_ReturnsFalse()
        {
            JsonData jsonData = new();

            // Act
            bool deleted = jsonData.DeleteById(42);

            // Assert
            Assert.False(deleted);
        }

        #endregion
    }
}
