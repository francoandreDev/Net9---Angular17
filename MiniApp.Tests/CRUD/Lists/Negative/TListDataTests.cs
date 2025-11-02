using MiniApp.CRUD.Lists;

namespace MiniApp.Tests.CRUD.Lists.Negative
{
    public abstract class ListDataNegativeTests<T>
    {
        protected abstract ListData<T> CreateListData();
        protected abstract T SampleItem { get; }

        [Fact]
        public async Task Update_InvalidIndex_ShouldThrowException()
        {
            var listData = CreateListData();
            await listData.CreateAsync(SampleItem);

            await Assert.ThrowsAsync<IndexOutOfRangeException>(
                async () => await listData.UpdateAsync(10, SampleItem)
            );
        }

        [Fact]
        public async Task Delete_InvalidIndex_ShouldThrowException()
        {
            var listData = CreateListData();
            await listData.CreateAsync(SampleItem);

            await Assert.ThrowsAsync<IndexOutOfRangeException>(
                async () => await listData.DeleteAsync(-1)
            );
        }
    }
}


