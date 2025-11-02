using MiniApp.CRUD.Lists;

namespace MiniApp.Tests.CRUD.Lists.Negative
{
    public class IntListDataNegativeTests : ListDataNegativeTests<int>
    {
        protected override ListData<int> CreateListData() => new();
        protected override int SampleItem => 42;
    }
}