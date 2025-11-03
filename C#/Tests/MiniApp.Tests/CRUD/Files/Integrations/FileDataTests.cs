// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Integration tests for FileData ensuring end-to-end file CRUD operations work correctly.
// ***********************************************************************

using MiniApp.CRUD.Files;

namespace MiniApp.Tests.CRUD.Files.Integrations
{
    /// <summary>
    /// 🧩 Contains integration tests for <see cref="FileData"/> to verify
    /// that file-based CRUD operations behave correctly as a whole.
    /// </summary>
    public class FileDataIntegrationTests : IDisposable
    {
        private readonly string _testFile;
        private readonly FileData _fileData;

        // ===========================================================
        #region 🔧 Constructor and Setup
        // ===========================================================

        /// <summary>
        /// 🧱 Initializes a temporary test file and an instance of <see cref="FileData"/>.
        /// </summary>
        public FileDataIntegrationTests()
        {
            _testFile = Path.GetTempFileName();
            _fileData = new FileData(_testFile);
        }

        #endregion

        // ===========================================================
        #region 🔁 Integration Tests
        // ===========================================================

        /// <summary>
        /// ✅ Verifies that creating and reading all entries works as expected.
        /// </summary>
        [Fact]
        public async Task CreateAndReadAll_ShouldWorkCorrectly()
        {
            await _fileData.CreateAsync("Line 1");
            await _fileData.CreateAsync("Line 2");

            var result = await _fileData.ReadAllAsync();

            Assert.Equal(2, result.Count());
            Assert.Contains("Line 1", result);
            Assert.Contains("Line 2", result);
        }

        /// <summary>
        /// ✏️ Ensures that updating a specific line modifies the correct entry in the file.
        /// </summary>
        [Fact]
        public async Task Update_ShouldModifyCorrectLine()
        {
            await _fileData.CreateAsync("A");
            await _fileData.CreateAsync("B");

            await _fileData.UpdateAsync(1, "B_modified");

            var result = await _fileData.ReadAllAsync();
            Assert.Equal("B_modified", result.ElementAt(1));
        }

        /// <summary>
        /// 🗑️ Ensures that deleting a specific line removes the correct item from the file.
        /// </summary>
        [Fact]
        public async Task Delete_ShouldRemoveCorrectLine()
        {
            await _fileData.CreateAsync("X");
            await _fileData.CreateAsync("Y");
            await _fileData.CreateAsync("Z");

            await _fileData.DeleteAsync(1);

            var result = await _fileData.ReadAllAsync();
            Assert.DoesNotContain("Y", result);
            Assert.Equal(2, result.Count());
        }

        #endregion

        // ===========================================================
        #region 🧹 Cleanup
        // ===========================================================

        /// <summary>
        /// 🧽 Cleans up the temporary test file after the integration tests are executed.
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
