namespace MiniApp.CRUD.Lists
{
    /// <summary>
    /// Clase base genérica para operaciones CRUD sobre listas.
    /// </summary>
    /// <typeparam name="T">Tipo de elemento de la lista</typeparam>
    public abstract class ListAbstract<T>
    {
        protected readonly List<T> Items;

        protected ListAbstract()
        {
            Items = new List<T>();
        }

        // Crear un nuevo elemento
        public abstract Task CreateAsync(T item);

        // Leer todos los elementos
        public abstract Task<IEnumerable<T>> ReadAllAsync();

        // Actualizar un elemento por índice
        public abstract Task UpdateAsync(int index, T item);

        // Eliminar un elemento por índice
        public abstract Task DeleteAsync(int index);

        // Obtener la cantidad de elementos
        public int Count => Items.Count;
    }
}
