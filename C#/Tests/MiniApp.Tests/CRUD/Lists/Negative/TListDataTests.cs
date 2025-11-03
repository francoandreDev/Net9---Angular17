// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Negative tests for ListData<T>, ensuring proper exceptions are thrown on invalid operations.
// ***********************************************************************

using MiniApp.CRUD.Lists.Base;

namespace MiniApp.Tests.CRUD.Lists.Negative
{
    /// <summary>
    /// 🛑 Base class for negative tests of <see cref="ListData{T}"/>.
    /// Tests that invalid operations throw the expected exceptions.
    /// </summary>
    /// <typeparam name="T">Type of list items.</typeparam>
    public abstract class ListDataNegativeTests<T>
    {
        #region ⚙️ Abstract Members

        /// <summary>
        /// Creates a new instance of <see cref="ListData{T}"/> for testing.
        /// </summary>
        protected abstract ListData<T> CreateListData();

        /// <summary>
        /// Sample item used for negative testing.
        /// </summary>
        protected abstract T SampleItem { get; }

        #endregion

        #region ❌ UPDATE NEGATIVE TESTS

        /// <summary>
        /// Verifies that updating an item at an invalid index throws <see cref="IndexOutOfRangeException"/>.
        /// </summary>
        [Fact]
        public async Task Update_InvalidIndex_ShouldThrowException()
        {
            // Arrange
            var listData = CreateListData();
            await listData.CreateAsync(SampleItem);

            // Act & Assert
            await Assert.ThrowsAsync<IndexOutOfRangeException>(
                async () => await listData.UpdateAsync(10, SampleItem)
            );
        }

        #endregion

        #region ❌ DELETE NEGATIVE TESTS

        /// <summary>
        /// Verifies that deleting an item at an invalid index throws <see cref="IndexOutOfRangeException"/>.
        /// </summary>
        [Fact]
        public async Task Delete_InvalidIndex_ShouldThrowException()
        {
            // Arrange
            var listData = CreateListData();
            await listData.CreateAsync(SampleItem);

            // Act & Assert
            await Assert.ThrowsAsync<IndexOutOfRangeException>(
                async () => await listData.DeleteAsync(-1)
            );
        }

        #endregion
    }
}
