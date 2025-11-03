// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Negative tests for ListData<int>, ensuring exceptions are thrown for invalid operations.
// ***********************************************************************

using MiniApp.CRUD.Lists.Base;

namespace MiniApp.Tests.CRUD.Lists.Negative
{
    /// <summary>
    /// 🛑 Negative tests for <see cref="ListData{T}"/> using <see cref="int"/> as the generic type.
    /// Inherits base negative tests from <see cref="ListDataNegativeTests{T}"/>.
    /// </summary>
    public class IntListDataNegativeTests : ListDataNegativeTests<int>
    {
        #region ⚙️ Setup

        /// <summary>
        /// Creates a new <see cref="ListData{T}"/> instance for integer data.
        /// </summary>
        protected override ListData<int> CreateListData() => new();

        /// <summary>
        /// Sample integer used in negative test scenarios.
        /// </summary>
        protected override int SampleItem => 42;

        #endregion
    }
}
