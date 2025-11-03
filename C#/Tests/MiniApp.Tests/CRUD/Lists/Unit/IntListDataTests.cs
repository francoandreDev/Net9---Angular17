using MiniApp.CRUD.Lists.Base;

namespace MiniApp.Tests.CRUD.Lists.Unit
{
    public class IntListDataTests : ListDataTests<int>
    {
        protected override ListData<int> CreateListData() => new();

        protected override int SampleItem1 => 10;
        protected override int SampleItem2 => 20;
        protected override int SampleItem3 => 30;
    }
}
