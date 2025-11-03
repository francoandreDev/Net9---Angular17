namespace MiniApp.CRUD.Lists.Base
{
    /// <summary>
    /// Simple in-memory implementation of <see cref="ListAbstract{T}"/> using a <see cref="List{T}"/>.
    /// </summary>Z
    /// <typeparam name="T">The type of element stored in the list.</typeparam>
    public class ListData<T> : ListAbstract<T>
    {
        public override Task CreateAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");

            Items.Add(item);
            return Task.CompletedTask;
        }

        public override Task<IEnumerable<T>> ReadAllAsync()
        {
            return Task.FromResult(Items.AsEnumerable());
        }

        public override Task UpdateAsync(int index, T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");

            if (index < 0 || index >= Items.Count)
                throw new IndexOutOfRangeException("Index out of range.");

            Items[index] = item;
            return Task.CompletedTask;
        }

        public override Task DeleteAsync(int index)
        {
            if (index < 0 || index >= Items.Count)
                throw new IndexOutOfRangeException("Index out of range.");

            Items.RemoveAt(index);
            return Task.CompletedTask;
        }
    }
}
