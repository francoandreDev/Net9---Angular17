using MiniApp.CRUD.Files;

namespace MiniApp.Tests.CRUD.Files.Unit
{
    public class JsonDataTests : IDisposable
    {
        private readonly string _testFile;
        private readonly FileData _fileData;

        public JsonDataTests()
        {
            // Creamos un archivo temporal para pruebas
            _testFile = Path.GetTempFileName();
            _fileData = new FileData(_testFile);
        }

        [Fact]
        public async Task Create_ShouldAddNewItem()
        {
            await _fileData.CreateAsync("Item 1");
            var all = await _fileData.ReadAllAsync();

            Assert.Single(all);
            Assert.Equal("Item 1", all.First());
        }

        [Fact]
        public async Task ReadAll_ShouldReturnAllItems()
        {
            await _fileData.CreateAsync("A");
            await _fileData.CreateAsync("B");

            var all = (await _fileData.ReadAllAsync()).ToList();

            Assert.Equal(2, all.Count);
            Assert.Contains("A", all);
            Assert.Contains("B", all);
        }

        [Fact]
        public async Task Update_ShouldModifyCorrectItem()
        {
            await _fileData.CreateAsync("Original");
            await _fileData.UpdateAsync(0, "Modificado");

            var all = await _fileData.ReadAllAsync();
            Assert.Equal("Modificado", all.First());
        }

        [Fact]
        public async Task Delete_ShouldRemoveCorrectItem()
        {
            await _fileData.CreateAsync("Uno");
            await _fileData.CreateAsync("Dos");

            await _fileData.DeleteAsync(0);

            var all = await _fileData.ReadAllAsync();
            Assert.Single(all);
            Assert.Equal("Dos", all.First());
        }

        [Fact]
        public async Task CRUD_FullFlow_ShouldWorkCorrectly()
        {
            // Create
            await _fileData.CreateAsync("1");
            await _fileData.CreateAsync("2");
            await _fileData.CreateAsync("3");

            // Read
            var all = (await _fileData.ReadAllAsync()).ToList();
            Assert.Equal(3, all.Count);

            // Update
            await _fileData.UpdateAsync(1, "2_modificado");
            all = (await _fileData.ReadAllAsync()).ToList();
            Assert.Equal("2_modificado", all[1]);

            // Delete
            await _fileData.DeleteAsync(0);
            all = (await _fileData.ReadAllAsync()).ToList();
            Assert.Equal(2, all.Count);
            Assert.DoesNotContain("1", all);
        }

        public void Dispose()
        {
            if (File.Exists(_testFile))
                File.Delete(_testFile);

            GC.SuppressFinalize(this);
        }
    }
}
