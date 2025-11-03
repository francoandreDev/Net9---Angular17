// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : 🧩 Integration tests for JsonData — verifying full CRUD behavior and data consistency.
// ***********************************************************************

using System.Text.Json.Nodes;
using MiniApp.CRUD.Jsons;

namespace MiniApp.Tests.CRUD.Jsons.Integrations
{
    /// <summary>
    /// 🧪 Integration test suite for <see cref="JsonData"/>.
    /// Ensures consistent behavior across Create, Read, Update, and Delete operations.
    /// </summary>
    public class JsonDataIntegrationTests
    {
        // ============================================================
        #region 🏭 Factory Setup
        // ============================================================

        /// <summary>
        /// Creates a fresh <see cref="JsonData"/> instance for each test.
        /// </summary>
        /// <returns>A new <see cref="JsonData"/> object.</returns>
        private static JsonData CreateJsonData() => new();

        #endregion

        // ============================================================
        #region 🔄 CRUD Flow Tests
        // ============================================================

        /// <summary>
        /// ✅ Verifies the complete CRUD sequence on <see cref="JsonData"/>.
        /// Ensures that each operation updates internal state as expected.
        /// </summary>
        [Fact]
        public void FullCrudFlow_ShouldBehaveConsistently()
        {
            // Arrange
            JsonData jsonData = CreateJsonData();

            // --- 🟢 CREATE ---
            JsonObject obj = new()
            {
                ["id"] = 100,
                ["name"] = "Original"
            };
            jsonData.Add(obj);

            // --- 🔵 READ ---
            JsonObject? found = jsonData.SearchById(100);
            Assert.NotNull(found);
            Assert.Equal("Original", found?["name"]?.ToString());

            // --- 🟡 UPDATE ---
            JsonObject updated = new()
            {
                ["name"] = "Updated"
            };
            bool updatedOk = jsonData.UpdateById(100, updated);
            Assert.True(updatedOk);

            // --- 🔍 VERIFY UPDATE ---
            JsonObject? afterUpdate = jsonData.SearchById(100);
            Assert.NotNull(afterUpdate);
            Assert.Equal("Updated", afterUpdate?["name"]?.ToString());

            // --- 🔴 DELETE ---
            bool deleted = jsonData.DeleteById(100);
            Assert.True(deleted);

            // --- 🧾 VERIFY DELETE ---
            JsonObject? afterDelete = jsonData.SearchById(100);
            Assert.Null(afterDelete);
        }

        #endregion
    }
}
