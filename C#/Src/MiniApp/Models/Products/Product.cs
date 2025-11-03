namespace MiniApp.Models.Products
{
    /// <summary>
    /// Represents a product with identifying information, pricing, and stock availability.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <param name="name">The name or description of the product.</param>
    /// <param name="price">The unit price of the product.</param>
    /// <param name="stock">The number of units available in stock.</param>
    public class Product(int id, string name, decimal price, int stock) : IProduct
    {
        /// <inheritdoc/>
        public int Id { get; init; } = id;

        /// <inheritdoc/>
        public string Name { get; init; } = name;

        /// <inheritdoc/>
        public decimal Price { get; init; } = price;

        /// <inheritdoc/>
        public int Stock { get; init; } = stock;

        /// <summary>
        /// Determines whether the product is available for purchase.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the product has a positive price, a non-empty name,
        /// and at least one unit in stock; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAvailable()
        {
            return Price > 0 && Stock > 0 && !string.IsNullOrWhiteSpace(Name);
        }

        /// <summary>
        /// Returns a human-readable string representation of the product.
        /// </summary>
        /// <returns>
        /// A string containing the product name, price, and stock information.
        /// </returns>
        /// <remarks>
        /// Example format: <c>"Laptop - $999.99 (5 in stock)"</c>.
        /// </remarks>
        public override string ToString() =>
            $"{Name} - ${Price} ({Stock} in stock)";
    }
}
