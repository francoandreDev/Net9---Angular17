// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Unit tests for FileData CRUD operations, ensuring consistency and reliability.
// ***********************************************************************

using MiniApp.CRUD.Files;

namespace MiniApp.Tests.CRUD.Files.Unit
{
    /// <summary>
    /// 🧪 Unit test suite for <see cref="FileData"/> class.
    /// Verifies correct behavior of Create, Read, Update, and Delete methods.
    /// </summary>
    public class JsonDataTests : IDisposable
    {
        private readonly string _testFile;
        private readonly FileData _fileData;

        // ===========================================================
        #region 🔧 Constructor and Setup
        // ===========================================================

        /// <summary>
        /// ⚙️ Initializes the test by creating a temporary file
        /// and a new instance of <see cref="FileData"/>.
        /// </summary>
        public JsonDataTests()
        {
            _testFile = Path.GetTempFileName();
            _fileData = new FileData(_testFile);
        }

        #endregion

        // ===========================================================
        #region 🧩 CRUD Operation Tests
        // ===========================================================

        /// <summary>
        /// 🟢 Ensures that creating a new item correctly adds it to the file.
        /// </summary>
        [Fact]
        public async Task Create_ShouldAddNewItem()
        {
            await _fileData.CreateAsync("Item 1");
            var all = await _fileData.ReadAllAsync();

            Assert.Single(all);
            Assert.Equal("Item 1", all.First());
        }

        /// <summary>
        /// 🔍 Verifies that all created items can be read from the file.
        /// </summary>
        [Fact]
        public async Task ReadAll_ShouldReturnAllItems()
        {
            await _fileData.CreateAsync("A");
            await _fileData.CreateAsync("B");

            var all = (await _fileData.ReadAllAsync()).ToList();

            Assert.Equal(2, all.Count);
            Assert.Contains("A", all);
            Assert.Contains("B", all);
        }

        /// <summary>
        /// ✏️ Ensures that updating an existing item modifies the correct entry.
        /// </summary>
        [Fact]
        public async Task Update_ShouldModifyCorrectItem()
        {
            await _fileData.CreateAsync("Original");
            await _fileData.UpdateAsync(0, "Modified");

            var all = await _fileData.ReadAllAsync();
            Assert.Equal("Modified", all.First());
        }

        /// <summary>
        /// ❌ Ensures that deleting an item removes it from the file correctly.
        /// </summary>
        [Fact]
        public async Task Delete_ShouldRemoveCorrectItem()
        {
            await _fileData.CreateAsync("One");
            await _fileData.CreateAsync("Two");

            await _fileData.DeleteAsync(0);

            var all = await _fileData.ReadAllAsync();
            Assert.Single(all);
            Assert.Equal("Two", all.First());
        }

        /// <summary>
        /// 🔁 Performs a complete CRUD flow test to validate full consistency.
        /// </summary>
        [Fact]
        public async Task CRUD_FullFlow_ShouldWorkCorrectly()
        {
            // CREATE
            await _fileData.CreateAsync("1");
            await _fileData.CreateAsync("2");
            await _fileData.CreateAsync("3");

            // READ
            var all = (await _fileData.ReadAllAsync()).ToList();
            Assert.Equal(3, all.Count);

            // UPDATE
            await _fileData.UpdateAsync(1, "2_modified");
            all = (await _fileData.ReadAllAsync()).ToList();
            Assert.Equal("2_modified", all[1]);

            // DELETE
            await _fileData.DeleteAsync(0);
            all = (await _fileData.ReadAllAsync()).ToList();
            Assert.Equal(2, all.Count);
            Assert.DoesNotContain("1", all);
        }

        #endregion

        // ===========================================================
        #region 🧹 Cleanup
        // ===========================================================

        /// <summary>
        /// 🧽 Cleans up by deleting the temporary file after tests.
        /// </summary>
        public void Dispose()
        {
            if (File.Exists(_testFile))
                File.Delete(_testFile);

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
