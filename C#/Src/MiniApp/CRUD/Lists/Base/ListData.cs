namespace MiniApp.CRUD.Lists.Base
{
    /// <summary>
    /// Simple in-memory implementation of <see cref="ListAbstract{T}"/> using a <see cref="List{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of element stored in the list.</typeparam>
    public class ListData<T> : ListAbstract<T>
    {
        /// <summary>
        /// Adds a new item to the in-memory list.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>A completed task representing the operation.</returns>
        public override Task CreateAsync(T item)
        {
            Items.Add(item);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Retrieves all items currently stored in the list.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation, containing
        /// an enumerable copy of the current list items.
        /// </returns>
        public override Task<IEnumerable<T>> ReadAllAsync()
        {
            // Return a copy to prevent external modification
            return Task.FromResult(Items.AsEnumerable());
        }

        /// <summary>
        /// Updates an existing item at the specified index.
        /// </summary>
        /// <param name="index">The index of the item to update.</param>
        /// <param name="item">The new value to assign to that position.</param>
        /// <returns>A completed task representing the operation.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the specified index is invalid.</exception>
        public override Task UpdateAsync(int index, T item)
        {
            if (index < 0 || index >= Items.Count)
                throw new IndexOutOfRangeException("Index out of range.");

            Items[index] = item;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Deletes an item at the specified index from the list.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        /// <returns>A completed task representing the operation.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the specified index is invalid.</exception>
        public override Task DeleteAsync(int index)
        {
            if (index < 0 || index >= Items.Count)
                throw new IndexOutOfRangeException("Index out of range.");

            Items.RemoveAt(index);
            return Task.CompletedTask;
        }
    }
}
