namespace MiniApp.CRUD.Files
{
    /// <summary>
    /// Provides a generic abstract base class for performing CRUD operations on files.
    /// </summary>
    /// <typeparam name="T">
    /// The type of object handled by the file operations (e.g., string, User, etc.).
    /// </typeparam>
    public abstract class FileAbstract<T>
    {
        /// <summary>
        /// Gets the full path of the file to be managed.
        /// </summary>
        protected readonly string FilePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAbstract{T}"/> class.
        /// </summary>
        /// <param name="filePath">The path of the file to operate on.</param>
        protected FileAbstract(string filePath)
        {
            FilePath = filePath;
        }

        /// <summary>
        /// Creates a new item and saves it to the file.
        /// </summary>
        /// <param name="item">The item to create and persist.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public abstract Task CreateAsync(T item);

        /// <summary>
        /// Reads all items from the file.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// containing an enumerable collection of all items.
        /// </returns>
        public abstract Task<IEnumerable<T>> ReadAllAsync();

        /// <summary>
        /// Updates an existing item in the file.
        /// </summary>
        /// <param name="index">The index or identifier of the item to update.</param>
        /// <param name="item">The updated item data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public abstract Task UpdateAsync(int index, T item);

        /// <summary>
        /// Deletes an item from the file.
        /// </summary>
        /// <param name="index">The index or identifier of the item to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public abstract Task DeleteAsync(int index);

        /// <summary>
        /// Determines whether the target file exists.
        /// </summary>
        /// <returns><c>true</c> if the file exists; otherwise, <c>false</c>.</returns>
        public bool Exists() => File.Exists(FilePath);
    }
}
