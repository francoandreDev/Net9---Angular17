namespace MiniApp.CRUD.Files
{
    /// <summary>
    /// Clase base genérica para operaciones CRUD sobre archivos.
    /// T define el tipo de dato a manejar (por ejemplo, string, User, etc.)
    /// </summary>
    public abstract class FileAbstract<T>
    {
        protected readonly string FilePath;

        protected FileAbstract(string filePath)
        {
            FilePath = filePath;
        }

        // Crea un nuevo elemento en el archivo
        public abstract Task CreateAsync(T item);

        // Lee todos los elementos del archivo
        public abstract Task<IEnumerable<T>> ReadAllAsync();

        // Actualiza un elemento existente (por id o índice, según la implementación)
        public abstract Task UpdateAsync(int index, T item);

        // Elimina un elemento por índice
        public abstract Task DeleteAsync(int index);

        // Comprueba si el archivo existe
        public bool Exists() => File.Exists(FilePath);
    }
}
