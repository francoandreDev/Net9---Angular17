// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : [francoandreDev 🧑‍💻]
// Created          : 2025-11-03
// Description      : Base class for testing CRUD operations in ListData<T>.
// ***********************************************************************

using MiniApp.CRUD.Lists.Base;

namespace MiniApp.Tests.CRUD.Lists.Unit
{
    /// <summary>
    /// 🧩 Generic base class for unit testing CRUD operations in <see cref="ListData{T}"/>.
    /// Provides a standard test set to verify create, read, update, and delete behavior.
    /// </summary>
    /// <typeparam name="T">Type of the list item being tested.</typeparam>
    public abstract class ListDataTests<T>
    {
        #region ⚙️ Abstract Members

        /// <summary>
        /// Creates a new instance of <see cref="ListData{T}"/> for running the tests.
        /// </summary>
        /// <returns>A concrete instance of <see cref="ListData{T}"/>.</returns>
        protected abstract ListData<T> CreateListData();

        /// <summary>
        /// Sample item #1 for testing.
        /// </summary>
        protected abstract T SampleItem1 { get; }

        /// <summary>
        /// Sample item #2 for testing.
        /// </summary>
        protected abstract T SampleItem2 { get; }

        /// <summary>
        /// Sample item #3 for testing.
        /// </summary>
        protected abstract T SampleItem3 { get; }

        #endregion

        #region 🧩 CREATE

        /// <summary>
        /// ✅ Verifies that <see cref="ListData{T}.CreateAsync"/> correctly adds an item to the list.
        /// </summary>
        [Fact]
        public async Task Create_ShouldAddItem()
        {
            // Arrange
            var listData = CreateListData();

            // Act
            await listData.CreateAsync(SampleItem1);
            var all = await listData.ReadAllAsync();

            // Assert
            Assert.Single(all);
            Assert.Equal(SampleItem1, all.First());
        }

        #endregion

        #region 🔍 READ

        /// <summary>
        /// 📖 Ensures that <see cref="ListData{T}.ReadAllAsync"/> returns all created items.
        /// </summary>
        [Fact]
        public async Task ReadAll_ShouldReturnAllItems()
        {
            // Arrange
            var listData = CreateListData();

            // Act
            await listData.CreateAsync(SampleItem1);
            await listData.CreateAsync(SampleItem2);

            var all = (await listData.ReadAllAsync()).ToList();

            // Assert
            Assert.Equal(2, all.Count);
            Assert.Contains(SampleItem1, all);
            Assert.Contains(SampleItem2, all);
        }

        #endregion

        #region ♻️ UPDATE

        /// <summary>
        /// 🔧 Verifies that <see cref="ListData{T}.UpdateAsync"/> correctly replaces the item at the given index.
        /// </summary>
        [Fact]
        public async Task Update_ShouldModifyCorrectItem()
        {
            // Arrange
            var listData = CreateListData();
            await listData.CreateAsync(SampleItem1);

            // Act
            await listData.UpdateAsync(0, SampleItem2);
            var all = await listData.ReadAllAsync();

            // Assert
            Assert.Equal(SampleItem2, all.First());
        }

        #endregion

        #region 🗑️ DELETE

        /// <summary>
        /// 🧹 Verifies that <see cref="ListData{T}.DeleteAsync"/> removes the correct item from the list.
        /// </summary>
        [Fact]
        public async Task Delete_ShouldRemoveCorrectItem()
        {
            // Arrange
            var listData = CreateListData();
            await listData.CreateAsync(SampleItem1);
            await listData.CreateAsync(SampleItem2);

            // Act
            await listData.DeleteAsync(0);
            var all = await listData.ReadAllAsync();

            // Assert
            Assert.Single(all);
            Assert.Equal(SampleItem2, all.First());
        }

        #endregion

        #region 🔁 FULL CRUD FLOW

        /// <summary>
        /// 🧠 Full end-to-end test covering create, read, update, and delete operations in sequence.
        /// Ensures that the entire CRUD flow works as expected.
        /// </summary>
        [Fact]
        public async Task CRUD_FullFlow_ShouldWorkCorrectly()
        {
            // Arrange
            var listData = CreateListData();

            // CREATE
            await listData.CreateAsync(SampleItem1);
            await listData.CreateAsync(SampleItem2);
            await listData.CreateAsync(SampleItem3);

            var all = (await listData.ReadAllAsync()).ToList();
            Assert.Equal(3, all.Count);

            // UPDATE
            await listData.UpdateAsync(1, SampleItem3);
            all = [.. await listData.ReadAllAsync()];
            Assert.Equal(SampleItem3, all[1]);

            // DELETE
            await listData.DeleteAsync(0);
            all = [.. await listData.ReadAllAsync()];

            // ASSERT
            Assert.Equal(2, all.Count);
            Assert.DoesNotContain(SampleItem1, all);
        }

        #endregion
    }
}
