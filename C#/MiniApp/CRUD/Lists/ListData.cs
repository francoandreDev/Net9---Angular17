namespace MiniApp.CRUD.Lists
{
    /// <summary>
    /// Implementación simple de ListAbstract usando List<T> en memoria.
    /// </summary>
    /// <typeparam name="T">Tipo de elemento de la lista</typeparam>
    public class ListData<T> : ListAbstract<T>
    {
        public override Task CreateAsync(T item)
        {
            Items.Add(item);
            return Task.CompletedTask;
        }

        public override Task<IEnumerable<T>> ReadAllAsync()
        {
            // Retornamos una copia para evitar modificación externa
            return Task.FromResult(Items.AsEnumerable());
        }

        public override Task UpdateAsync(int index, T item)
        {
            if (index < 0 || index >= Items.Count)
                throw new IndexOutOfRangeException("Índice fuera de rango.");

            Items[index] = item;
            return Task.CompletedTask;
        }

        public override Task DeleteAsync(int index)
        {
            if (index < 0 || index >= Items.Count)
                throw new IndexOutOfRangeException("Índice fuera de rango.");

            Items.RemoveAt(index);
            return Task.CompletedTask;
        }
    }
}
