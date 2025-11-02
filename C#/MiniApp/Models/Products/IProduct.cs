namespace MiniApp.Models.Products
{
    public interface IProduct
    {
        int Id { get; }
        string Name { get; }
        decimal Price { get; }
        int Stock { get; }

        bool IsAvailable();
    }
}
