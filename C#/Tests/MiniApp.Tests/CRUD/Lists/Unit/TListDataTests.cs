using MiniApp.CRUD.Lists.Base;

namespace MiniApp.Tests.CRUD.Lists.Unit
{
    /// <summary>
    /// Clase base genérica para pruebas CRUD de ListData&lt;T&gt;.
    /// </summary>
    /// <typeparam name="T">Tipo de elemento de la lista</typeparam>
    public abstract class ListDataTests<T>
    {
        protected abstract ListData<T> CreateListData();
        protected abstract T SampleItem1 { get; }
        protected abstract T SampleItem2 { get; }
        protected abstract T SampleItem3 { get; }

        [Fact]
        public async Task Create_ShouldAddItem()
        {
            var listData = CreateListData();
            await listData.CreateAsync(SampleItem1);

            var all = await listData.ReadAllAsync();
            Assert.Single(all);
            Assert.Equal(SampleItem1, all.First());
        }

        [Fact]
        public async Task ReadAll_ShouldReturnAllItems()
        {
            var listData = CreateListData();
            await listData.CreateAsync(SampleItem1);
            await listData.CreateAsync(SampleItem2);

            var all = (await listData.ReadAllAsync()).ToList();
            Assert.Equal(2, all.Count);
            Assert.Contains(SampleItem1, all);
            Assert.Contains(SampleItem2, all);
        }

        [Fact]
        public async Task Update_ShouldModifyCorrectItem()
        {
            var listData = CreateListData();
            await listData.CreateAsync(SampleItem1);
            await listData.UpdateAsync(0, SampleItem2);

            var all = await listData.ReadAllAsync();
            Assert.Equal(SampleItem2, all.First());
        }

        [Fact]
        public async Task Delete_ShouldRemoveCorrectItem()
        {
            var listData = CreateListData();
            await listData.CreateAsync(SampleItem1);
            await listData.CreateAsync(SampleItem2);

            await listData.DeleteAsync(0);
            var all = await listData.ReadAllAsync();

            Assert.Single(all);
            Assert.Equal(SampleItem2, all.First());
        }

        [Fact]
        public async Task CRUD_FullFlow_ShouldWorkCorrectly()
        {
            var listData = CreateListData();

            await listData.CreateAsync(SampleItem1);
            await listData.CreateAsync(SampleItem2);
            await listData.CreateAsync(SampleItem3);

            var all = (await listData.ReadAllAsync()).ToList();
            Assert.Equal(3, all.Count);

            await listData.UpdateAsync(1, SampleItem3);
            all = [.. await listData.ReadAllAsync()];
            Assert.Equal(SampleItem3, all[1]);

            await listData.DeleteAsync(0);
            all = [.. await listData.ReadAllAsync()];
            Assert.Equal(2, all.Count);
            Assert.DoesNotContain(SampleItem1, all);
        }
    }
}
