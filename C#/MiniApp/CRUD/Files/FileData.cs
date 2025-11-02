namespace MiniApp.CRUD.Files
{
    /// <summary>
    /// Implementación simple de FileAbstract que maneja datos de texto (línea por línea).
    /// </summary>
    public class FileData : FileAbstract<string>
    {
        public FileData(string filePath) : base(filePath)
        {
            // Si el archivo no existe, lo creamos vacío
            if (!File.Exists(FilePath))
                File.WriteAllText(FilePath, string.Empty);
        }

        public override async Task CreateAsync(string item)
        {
            await File.AppendAllTextAsync(FilePath, item + Environment.NewLine);
        }

        public override async Task<IEnumerable<string>> ReadAllAsync()
        {
            var lines = await File.ReadAllLinesAsync(FilePath);
            return lines;
        }

        public override async Task UpdateAsync(int index, string item)
        {
            var lines = (await File.ReadAllLinesAsync(FilePath)).ToList();

            if (index < 0 || index >= lines.Count)
                throw new IndexOutOfRangeException("Índice fuera de rango.");

            lines[index] = item;
            await File.WriteAllLinesAsync(FilePath, lines);
        }

        public override async Task DeleteAsync(int index)
        {
            var lines = (await File.ReadAllLinesAsync(FilePath)).ToList();

            if (index < 0 || index >= lines.Count)
                throw new IndexOutOfRangeException("Índice fuera de rango.");

            lines.RemoveAt(index);
            await File.WriteAllLinesAsync(FilePath, lines);
        }
    }
}
