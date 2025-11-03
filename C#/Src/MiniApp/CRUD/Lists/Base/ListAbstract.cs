namespace MiniApp.CRUD.Lists.Base
{
    /// <summary>
    /// Provides a generic abstract base class for performing CRUD operations on an in-memory list.
    /// </summary>
    /// <typeparam name="T">The type of items stored in the list.</typeparam>
    public abstract class ListAbstract<T>
    {
        /// <summary>
        /// Internal collection that stores all items.
        /// </summary>
        protected readonly List<T> Items;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAbstract{T}"/> class
        /// with an empty <see cref="List{T}"/>.
        /// </summary>
        protected ListAbstract()
        {
            Items = [];
        }

        /// <summary>
        /// Creates a new item and adds it to the list.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public abstract Task CreateAsync(T item);

        /// <summary>
        /// Reads all items currently stored in the list.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation,
        /// containing an enumerable collection of all items.
        /// </returns>
        public abstract Task<IEnumerable<T>> ReadAllAsync();

        /// <summary>
        /// Updates an existing item at the specified index.
        /// </summary>
        /// <param name="index">The index of the item to update.</param>
        /// <param name="item">The updated item value.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public abstract Task UpdateAsync(int index, T item);

        /// <summary>
        /// Deletes an item from the list at the specified index.
        /// </summary>
        /// <param name="index">The index of the item to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public abstract Task DeleteAsync(int index);

        /// <summary>
        /// Gets the total number of items currently stored in the list.
        /// </summary>
        public int Count => Items.Count;
    }
}
