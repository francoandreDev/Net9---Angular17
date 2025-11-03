// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Integration tests for ListData<int> CRUD operations
// ***********************************************************************

using MiniApp.CRUD.Lists.Base;

namespace MiniApp.Tests.CRUD.Lists.Integration
{
    /// <summary>
    /// 🔗 Integration tests for <see cref="ListData{T}"/> using <see cref="int"/> items.
    /// Validates the full CRUD flow: Create → Read → Update → Delete.
    /// </summary>
    public class IntListDataIntegrationTests : ListDataIntegrationTests<int>
    {
        #region 🛠 Factory Method

        /// <summary>
        /// Creates a new <see cref="ListData{int}"/> instance for integration testing.
        /// </summary>
        /// <returns>
        /// A new instance of <see cref="ListData{int}"/>.
        /// </returns>
        protected override ListData<int> CreateListData() => new();

        #endregion

        #region 📦 Sample Data

        /// <summary>
        /// First sample integer used in the CRUD flow tests.
        /// </summary>
        protected override int SampleItem1 => 10;

        /// <summary>
        /// Second sample integer used in the CRUD flow tests.
        /// </summary>
        protected override int SampleItem2 => 20;

        /// <summary>
        /// Third sample integer used in the CRUD flow tests.
        /// </summary>
        protected override int SampleItem3 => 30;

        #endregion
    }
}
