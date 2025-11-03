// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Unit tests for JsonEntity to validate JSON conversion and entity rules.
// ***********************************************************************

using System.Text.Json.Nodes;
using MiniApp.Models.Json;

namespace MiniApp.Tests.Models.Json
{
    /// <summary>
    /// 🧩 Provides unit tests for <see cref="JsonEntity"/>, ensuring its
    /// validation logic and JSON serialization/deserialization are correct.
    /// </summary>
    public class JsonEntityTests
    {
        // ===========================================================
        #region ✅ Validation Tests
        // ===========================================================

        /// <summary>
        /// ✅ Ensures that <see cref="JsonEntity.IsValid"/> returns <c>true</c>
        /// when both <c>Id</c> and <c>Name</c> are valid.
        /// </summary>
        [Fact]
        public void IsValid_ShouldReturnTrue_WhenIdAndNameAreValid()
        {
            var entity = new JsonEntity(1, "Test");
            Assert.True(entity.IsValid());
        }

        /// <summary>
        /// 🚫 Ensures that <see cref="JsonEntity.IsValid"/> returns <c>false</c>
        /// when given invalid ID or Name values.
        /// </summary>
        /// <param name="id">The entity identifier to test.</param>
        /// <param name="name">The entity name to test.</param>
        [Theory]
        [InlineData(0, "ValidName")]
        [InlineData(-5, "ValidName")]
        [InlineData(1, "")]
        [InlineData(1, "   ")]
        public void IsValid_ShouldReturnFalse_ForInvalidValues(int id, string name)
        {
            var entity = new JsonEntity(id, name);
            Assert.False(entity.IsValid());
        }

        #endregion

        // ===========================================================
        #region 🔄 JSON Serialization Tests
        // ===========================================================

        /// <summary>
        /// 🧱 Verifies that <see cref="JsonEntity.ToJson"/> correctly converts
        /// the entity into a valid <see cref="JsonObject"/>.
        /// </summary>
        [Fact]
        public void ToJson_ShouldReturnExpectedJsonObject()
        {
            var entity = new JsonEntity(10, "Alice");
            var json = entity.ToJson();

            Assert.Equal(10, json["id"]!.GetValue<int>());
            Assert.Equal("Alice", json["name"]!.GetValue<string>());
        }

        /// <summary>
        /// 🔄 Ensures that <see cref="JsonEntity.FromJson"/> properly creates
        /// a valid entity from a complete <see cref="JsonObject"/>.
        /// </summary>
        [Fact]
        public void FromJson_ShouldReturnValidEntity_WhenJsonIsValid()
        {
            var json = new JsonObject
            {
                ["id"] = 42,
                ["name"] = "Bob"
            };

            var entity = JsonEntity.FromJson(json);

            Assert.NotNull(entity);
            Assert.Equal(42, entity!.Id);
            Assert.Equal("Bob", entity.Name);
        }

        /// <summary>
        /// 🚫 Confirms that <see cref="JsonEntity.FromJson"/> returns <c>null</c>
        /// when the input JSON is <c>null</c>.
        /// </summary>
        [Fact]
        public void FromJson_ShouldReturnNull_WhenJsonIsNull()
        {
            var entity = JsonEntity.FromJson(null);
            Assert.Null(entity);
        }

        /// <summary>
        /// ⚠️ Ensures that <see cref="JsonEntity.FromJson"/> returns <c>null</c>
        /// if the JSON object is missing required fields.
        /// </summary>
        [Fact]
        public void FromJson_ShouldReturnNull_WhenJsonMissingFields()
        {
            var json = new JsonObject
            {
                ["id"] = 5
            };

            var entity = JsonEntity.FromJson(json);
            Assert.Null(entity);
        }

        #endregion
    }
}
