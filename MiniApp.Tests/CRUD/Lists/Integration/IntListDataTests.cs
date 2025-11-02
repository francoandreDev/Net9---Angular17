using MiniApp.CRUD.Lists;

namespace MiniApp.Tests.CRUD.Lists.Integration
{
    public class IntListDataIntegrationTests : ListDataIntegrationTests<int>
    {
        protected override ListData<int> CreateListData() => new();
        protected override int SampleItem1 => 10;
        protected override int SampleItem2 => 20;
        protected override int SampleItem3 => 30;
    }
}