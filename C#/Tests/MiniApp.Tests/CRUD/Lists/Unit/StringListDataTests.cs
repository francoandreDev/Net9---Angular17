// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : [francoandreDev 🧑‍💻]
// Created          : 2025-11-03
// Description      : Unit tests for ListData<string>.
// ***********************************************************************

using MiniApp.CRUD.Lists.Base;

namespace MiniApp.Tests.CRUD.Lists.Unit
{
    /// <summary>
    /// 🧪 Unit tests for <see cref="ListData{T}"/> using <see cref="string"/> as the generic type.
    /// Inherits the full CRUD test suite from <see cref="ListDataTests{T}"/>.
    /// </summary>
    public class StringListDataTests : ListDataTests<string>
    {
        #region ⚙️ Setup

        /// <summary>
        /// Creates a new <see cref="ListData{T}"/> instance for string data.
        /// </summary>
        /// <returns>A new instance of <see cref="ListData{string}"/>.</returns>
        protected override ListData<string> CreateListData() => new();

        #endregion

        #region 🧩 Sample Data

        /// <summary>
        /// Sample item #1: represents the first test string.
        /// </summary>
        protected override string SampleItem1 => "Alpha";

        /// <summary>
        /// Sample item #2: represents the second test string.
        /// </summary>
        protected override string SampleItem2 => "Beta";

        /// <summary>
        /// Sample item #3: represents the third test string.
        /// </summary>
        protected override string SampleItem3 => "Gamma";

        #endregion
    }
}
