// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Integration tests for ListData<string> CRUD operations.
// ***********************************************************************

using MiniApp.CRUD.Lists.Base;

namespace MiniApp.Tests.CRUD.Lists.Integration
{
    /// <summary>
    /// 🔗 Integration tests for <see cref="ListData{T}"/> using <see cref="string"/> items.
    /// Ensures the full CRUD flow (Create → Read → Update → Delete) works as expected
    /// with simple string-based data.
    /// </summary>
    public class StringListDataIntegrationTests : ListDataIntegrationTests<string>
    {
        #region 🏭 Factory Method
        // ---------------------------------------------------------------------
        // Factory Method
        // ---------------------------------------------------------------------

        /// <summary>
        ///     🧪 Creates a new <see cref="ListData{string}"/> instance for testing.
        /// </summary>
        /// <returns>A fresh instance of <see cref="ListData{string}"/>.</returns>
        protected override ListData<string> CreateListData() => new();
        #endregion

        #region 📦 Sample Data
        // ---------------------------------------------------------------------
        // Sample Data
        // ---------------------------------------------------------------------

        /// <summary>
        ///     🧩 First sample string for testing.
        /// </summary>
        protected override string SampleItem1 => "Alpha";

        /// <summary>
        ///     🧩 Second sample string for testing.
        /// </summary>
        protected override string SampleItem2 => "Beta";

        /// <summary>
        ///     🧩 Third sample string for testing.
        /// </summary>
        protected override string SampleItem3 => "Gamma";
        #endregion
    }
}
