using MiniApp.CRUD.Lists;

namespace MiniApp.Tests.CRUD.Lists.Negative
{
    public class StringListDataNegativeTests : ListDataNegativeTests<string>
    {
        protected override ListData<string> CreateListData() => new();
        protected override string SampleItem => "Test";
    }
}