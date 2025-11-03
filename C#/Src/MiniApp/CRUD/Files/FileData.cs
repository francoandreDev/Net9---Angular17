namespace MiniApp.CRUD.Files
{
    /// <summary>
    /// Simple implementation of <see cref="FileAbstract{T}"/> that manages text data line by line.
    /// </summary>
    public class FileData : FileAbstract<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileData"/> class.
        /// Creates an empty file if it does not already exist.
        /// </summary>
        /// <param name="filePath">The path of the text file to manage.</param>
        public FileData(string filePath) : base(filePath)
        {
            if (!File.Exists(FilePath))
                File.WriteAllText(FilePath, string.Empty);
        }

        /// <summary>
        /// Appends a new line to the file.
        /// </summary>
        /// <param name="item">The text line to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task CreateAsync(string item)
        {
            await File.AppendAllTextAsync(FilePath, item + Environment.NewLine);
        }

        /// <summary>
        /// Reads all lines from the file.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation, containing all text lines.
        /// </returns>
        public override async Task<IEnumerable<string>> ReadAllAsync()
        {
            var lines = await File.ReadAllLinesAsync(FilePath);
            return lines;
        }

        /// <summary>
        /// Updates a specific line in the file at the given index.
        /// </summary>
        /// <param name="index">The line index to update.</param>
        /// <param name="item">The new text to replace the existing line.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the specified index is out of range.</exception>
        public override async Task UpdateAsync(int index, string item)
        {
            var lines = (await File.ReadAllLinesAsync(FilePath)).ToList();

            if (index < 0 || index >= lines.Count)
                throw new IndexOutOfRangeException("Index out of range.");

            lines[index] = item;
            await File.WriteAllLinesAsync(FilePath, lines);
        }

        /// <summary>
        /// Deletes the line at the specified index.
        /// </summary>
        /// <param name="index">The index of the line to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the specified index is out of range.</exception>
        public override async Task DeleteAsync(int index)
        {
            var lines = (await File.ReadAllLinesAsync(FilePath)).ToList();

            if (index < 0 || index >= lines.Count)
                throw new IndexOutOfRangeException("Index out of range.");

            lines.RemoveAt(index);
            await File.WriteAllLinesAsync(FilePath, lines);
        }
    }
}
