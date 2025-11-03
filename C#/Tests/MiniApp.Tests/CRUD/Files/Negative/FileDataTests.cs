// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Negative test cases for FileData to ensure proper exception handling.
// ***********************************************************************

using MiniApp.CRUD.Files;

namespace MiniApp.Tests.CRUD.Files.Negative
{
    /// <summary>
    /// ⚠️ Contains negative test scenarios for <see cref="FileData"/>,
    /// verifying that exceptions are properly thrown for invalid operations.
    /// </summary>
    public class JsonDataNegativeTests : IDisposable
    {
        private readonly string _testFile;
        private readonly FileData _fileData;

        // ===========================================================
        #region 🔧 Constructor and Setup
        // ===========================================================

        /// <summary>
        /// 🧱 Initializes a temporary file and a new instance of <see cref="FileData"/> for testing.
        /// </summary>
        public JsonDataNegativeTests()
        {
            _testFile = Path.GetTempFileName();
            _fileData = new FileData(_testFile);
        }

        #endregion

        // ===========================================================
        #region 🚫 Negative Behavior Tests
        // ===========================================================

        /// <summary>
        /// ❗ Ensures that updating an invalid index throws an <see cref="IndexOutOfRangeException"/>.
        /// </summary>
        [Fact]
        public async Task Update_InvalidIndex_ShouldThrowException()
        {
            await _fileData.CreateAsync("Valid data");

            await Assert.ThrowsAsync<IndexOutOfRangeException>(
                async () => await _fileData.UpdateAsync(10, "Non-existent item")
            );
        }

        /// <summary>
        /// ❌ Ensures that deleting an invalid index throws an <see cref="IndexOutOfRangeException"/>.
        /// </summary>
        [Fact]
        public async Task Delete_InvalidIndex_ShouldThrowException()
        {
            await _fileData.CreateAsync("Valid data");

            await Assert.ThrowsAsync<IndexOutOfRangeException>(
                async () => await _fileData.DeleteAsync(-1)
            );
        }

        /// <summary>
        /// 🧱 Ensures that trying to write while the file is locked throws an <see cref="IOException"/>.
        /// </summary>
        [Fact]
        public async Task Create_FileLocked_ShouldThrowIOException()
        {
            using var stream = new FileStream(_testFile, FileMode.Open, FileAccess.Read, FileShare.None);

            await Assert.ThrowsAsync<IOException>(
                async () => await _fileData.CreateAsync("Cannot write while locked")
            );
        }

        #endregion

        // ===========================================================
        #region 🧹 Cleanup
        // ===========================================================

        /// <summary>
        /// 🧽 Cleans up the temporary file after the tests are executed.
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
