using MiniApp.CRUD.Lists.Base;

namespace MiniApp.Tests.CRUD.Lists.Negative
{
    public class IntListDataNegativeTests : ListDataNegativeTests<int>
    {
        protected override ListData<int> CreateListData() => new();
        protected override int SampleItem => 42;
    }
}