using MiniApp.CRUD.Lists;

namespace MiniApp.Tests.CRUD.Lists.Unit
{
    public class StringListDataTests : ListDataTests<string>
    {
        protected override ListData<string> CreateListData() => new();

        protected override string SampleItem1 => "Alpha";
        protected override string SampleItem2 => "Beta";
        protected override string SampleItem3 => "Gamma";
    }
}
