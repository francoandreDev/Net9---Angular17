using MiniApp.CRUD.Files;

namespace MiniApp.Tests.CRUD.Files.Integrations
{
    public class FileDataIntegrationTests : IDisposable
    {
        private readonly string _testFile;
        private readonly FileData _fileData;

        public FileDataIntegrationTests()
        {
            _testFile = Path.GetTempFileName();
            _fileData = new FileData(_testFile);
        }

        [Fact]
        public async Task CreateAndReadAll_ShouldWorkCorrectly()
        {
            await _fileData.CreateAsync("Linea 1");
            await _fileData.CreateAsync("Linea 2");

            var result = await _fileData.ReadAllAsync();

            Assert.Equal(2, result.Count());
            Assert.Contains("Linea 1", result);
            Assert.Contains("Linea 2", result);
        }

        [Fact]
        public async Task Update_ShouldModifyCorrectLine()
        {
            await _fileData.CreateAsync("A");
            await _fileData.CreateAsync("B");

            await _fileData.UpdateAsync(1, "B_modificado");

            var result = await _fileData.ReadAllAsync();
            Assert.Equal("B_modificado", result.ElementAt(1));
        }

        [Fact]
        public async Task Delete_ShouldRemoveCorrectLine()
        {
            await _fileData.CreateAsync("X");
            await _fileData.CreateAsync("Y");
            await _fileData.CreateAsync("Z");

            await _fileData.DeleteAsync(1);

            var result = await _fileData.ReadAllAsync();
            Assert.DoesNotContain("Y", result);
            Assert.Equal(2, result.Count());
        }

        public void Dispose()
        {
            if (File.Exists(_testFile))
                File.Delete(_testFile);

            GC.SuppressFinalize(this);
        }
    }
}
