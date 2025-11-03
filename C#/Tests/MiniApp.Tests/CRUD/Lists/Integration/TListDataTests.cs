using MiniApp.CRUD.Lists.Base;

namespace MiniApp.Tests.CRUD.Lists.Integration
{
    /// <summary>
    /// Clase base genérica para pruebas de integración de ListData&lt;T&gt;.
    /// </summary>
    /// <typeparam name="T">Tipo de elemento de la lista</typeparam>
    public abstract class ListDataIntegrationTests<T>
    {
        protected abstract ListData<T> CreateListData();
        protected abstract T SampleItem1 { get; }
        protected abstract T SampleItem2 { get; }
        protected abstract T SampleItem3 { get; }

        [Fact]
        public async Task CRUD_FullFlow_ShouldWorkCorrectly()
        {
            var listData = CreateListData();

            // Create
            await listData.CreateAsync(SampleItem1);
            await listData.CreateAsync(SampleItem2);
            await listData.CreateAsync(SampleItem3);

            // Read
            var all = (await listData.ReadAllAsync()).ToList();
            Assert.Equal(3, all.Count);

            // Update
            await listData.UpdateAsync(1, SampleItem3);
            all = (await listData.ReadAllAsync()).ToList();
            Assert.Equal(SampleItem3, all[1]);

            // Delete
            await listData.DeleteAsync(0);
            all = (await listData.ReadAllAsync()).ToList();
            Assert.Equal(2, all.Count);
            Assert.DoesNotContain(SampleItem1, all);
        }
    }
}
