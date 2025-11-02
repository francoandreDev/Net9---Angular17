namespace MiniApp.Models.Products
{
    public class Product(int id, string name, decimal price, int stock) : IProduct
    {
        public int Id { get; init; } = id;
        public string Name { get; init; } = name;
        public decimal Price { get; init; } = price;
        public int Stock { get; init; } = stock;

        public bool IsAvailable()
        {
            return Price > 0 && Stock > 0 && !string.IsNullOrWhiteSpace(Name);
        }

        public override string ToString() =>
            $"{Name} - ${Price} ({Stock} en stock)";
    }
}
