// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : [francoandreDev 🧑‍💻]
// Created          : 2025-11-03
// Description      : Unit tests for ListData<int>.
// ***********************************************************************

using MiniApp.CRUD.Lists.Base;

namespace MiniApp.Tests.CRUD.Lists.Unit
{
    /// <summary>
    /// 🧪 Unit tests for <see cref="ListData{T}"/> using <see cref="int"/> as the generic type.
    /// Inherits the full CRUD test suite from <see cref="ListDataTests{T}"/>.
    /// </summary>
    public class IntListDataTests : ListDataTests<int>
    {
        #region ⚙️ Setup

        /// <summary>
        /// Creates a new <see cref="ListData{T}"/> instance for integer data.
        /// </summary>
        /// <returns>A new instance of <see cref="ListData{int}"/>.</returns>
        protected override ListData<int> CreateListData() => new();

        #endregion

        #region 🧩 Sample Data

        /// <summary>
        /// Sample item #1: represents the first test integer.
        /// </summary>
        protected override int SampleItem1 => 10;

        /// <summary>
        /// Sample item #2: represents the second test integer.
        /// </summary>
        protected override int SampleItem2 => 20;

        /// <summary>
        /// Sample item #3: represents the third test integer.
        /// </summary>
        protected override int SampleItem3 => 30;

        #endregion
    }
}
