using MiniApp.CRUD.Lists;

namespace MiniApp.Tests.CRUD.Lists.Integration
{
    public class StringListDataIntegrationTests : ListDataIntegrationTests<string>
    {
        protected override ListData<string> CreateListData() => new();
        protected override string SampleItem1 => "Alpha";
        protected override string SampleItem2 => "Beta";
        protected override string SampleItem3 => "Gamma";
    }
}