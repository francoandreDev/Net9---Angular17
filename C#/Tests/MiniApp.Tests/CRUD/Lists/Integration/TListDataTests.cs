// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Generic base class for integration tests of ListData<T> CRUD operations.
// ***********************************************************************

using MiniApp.CRUD.Lists.Base;

namespace MiniApp.Tests.CRUD.Lists.Integration
{
    /// <summary>
    /// 🔗 Generic base class for integration tests of <see cref="ListData{T}"/>.
    /// Ensures that CRUD operations (Create, Read, Update, Delete) work correctly
    /// in a real scenario.
    /// </summary>
    /// <typeparam name="T">The type of item stored in the list.</typeparam>
    public abstract class ListDataIntegrationTests<T>
    {
        #region 🏭 Factory Methods
        // ---------------------------------------------------------------------
        // Factory Methods
        // ---------------------------------------------------------------------

        /// <summary>
        ///     🧪 Creates a new instance of <see cref="ListData{T}"/> for testing.
        ///     <para>Implemented by concrete test classes to provide a ready-to-use instance.</para>
        /// </summary>
        /// <returns>A new instance of <see cref="ListData{T}"/>.</returns>
        protected abstract ListData<T> CreateListData();
        #endregion

        #region 📦 Sample Data
        // ---------------------------------------------------------------------
        // Sample Data
        // ---------------------------------------------------------------------

        /// <summary>
        ///     🧩 First sample item for testing purposes.
        /// </summary>
        protected abstract T SampleItem1 { get; }

        /// <summary>
        ///     🧩 Second sample item for testing purposes.
        /// </summary>
        protected abstract T SampleItem2 { get; }

        /// <summary>
        ///     🧩 Third sample item for testing purposes.
        /// </summary>
        protected abstract T SampleItem3 { get; }
        #endregion

        #region ✅ CRUD Flow Tests
        // ---------------------------------------------------------------------
        // CRUD Flow Tests
        // ---------------------------------------------------------------------

        /// <summary>
        ///     ✅ Full CRUD flow: Create → Read → Update → Delete.
        ///     <para>
        ///         Verifies that:
        ///         1) Items are correctly added.<br/>
        ///         2) Items can be retrieved as expected.<br/>
        ///         3) Updates properly replace existing entries.<br/>
        ///         4) Deletions remove the correct items.
        ///     </para>
        /// </summary>
        [Fact(DisplayName = "CRUD_FullFlow_ShouldWorkCorrectly")]
        public async Task CRUD_FullFlow_ShouldWorkCorrectly()
        {
            // Arrange
            var listData = CreateListData();

            // ➕ Create items
            await listData.CreateAsync(SampleItem1);
            await listData.CreateAsync(SampleItem2);
            await listData.CreateAsync(SampleItem3);

            // 👀 Read all items
            var all = (await listData.ReadAllAsync()).ToList();
            Assert.Equal(3, all.Count);

            // ✏️ Update the second item
            await listData.UpdateAsync(1, SampleItem3);
            all = (await listData.ReadAllAsync()).ToList();
            Assert.Equal(SampleItem3, all[1]);

            // ❌ Delete the first item
            await listData.DeleteAsync(0);
            all = (await listData.ReadAllAsync()).ToList();

            // Assertions
            Assert.Equal(2, all.Count);
            Assert.DoesNotContain(SampleItem1, all);
        }

        #endregion
    }
}
