namespace MiniApp.Models.Products
{
    /// <summary>
    /// Defines a contract for product entities, including
    /// identifying information, pricing, stock, and availability logic.
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// Gets the unique identifier of the product.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the display name of the product.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the price of the product.
        /// </summary>
        decimal Price { get; }

        /// <summary>
        /// Gets the number of units available in stock.
        /// </summary>
        int Stock { get; }

        /// <summary>
        /// Determines whether the product is currently available for purchase.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the product has stock available (typically <see cref="Stock"/> &gt; 0);
        /// otherwise, <c>false</c>.
        /// </returns>
        bool IsAvailable();
    }
}
