using MiniApp.CRUD.Files;

namespace MiniApp.Tests.CRUD.Files.Negative
{
    public class JsonDataNegativeTests : IDisposable
    {
        private readonly string _testFile;
        private readonly FileData _fileData;

        public JsonDataNegativeTests()
        {
            _testFile = Path.GetTempFileName();
            _fileData = new FileData(_testFile);
        }

        [Fact]
        public async Task Update_InvalidIndex_ShouldThrowException()
        {
            await _fileData.CreateAsync("Dato válido");

            await Assert.ThrowsAsync<IndexOutOfRangeException>(
                async () => await _fileData.UpdateAsync(10, "No existe")
            );
        }

        [Fact]
        public async Task Delete_InvalidIndex_ShouldThrowException()
        {
            await _fileData.CreateAsync("Dato válido");

            await Assert.ThrowsAsync<IndexOutOfRangeException>(
                async () => await _fileData.DeleteAsync(-1)
            );
        }

        [Fact]
        public async Task Create_FileLocked_ShouldThrowIOException()
        {
            using var stream = new FileStream(_testFile, FileMode.Open, FileAccess.Read, FileShare.None);

            await Assert.ThrowsAsync<IOException>(
                async () => await _fileData.CreateAsync("No se puede escribir")
            );
        }

        public void Dispose()
        {
            if (File.Exists(_testFile))
                File.Delete(_testFile);

            GC.SuppressFinalize(this);
        }
    }
}
