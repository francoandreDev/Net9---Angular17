// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Negative tests for ListData<string>, ensuring exceptions are thrown for invalid operations.
// ***********************************************************************

using MiniApp.CRUD.Lists.Base;

namespace MiniApp.Tests.CRUD.Lists.Negative
{
    /// <summary>
    /// 🛑 Negative tests for <see cref="ListData{T}"/> using <see cref="string"/> as the generic type.
    /// Inherits base negative tests from <see cref="ListDataNegativeTests{T}"/>.
    /// </summary>
    public class StringListDataNegativeTests : ListDataNegativeTests<string>
    {
        #region ⚙️ Setup

        /// <summary>
        /// Creates a new <see cref="ListData{T}"/> instance for string data.
        /// </summary>
        protected override ListData<string> CreateListData() => new();

        /// <summary>
        /// Sample string used in negative test scenarios.
        /// </summary>
        protected override string SampleItem => "Test";

        #endregion
    }
}
