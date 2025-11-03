using MiniApp.CRUD.Lists.Base;
using MiniApp.Models.Products;

namespace MiniApp.CRUD.Lists.ProductList
{
    /// <summary>
    /// In-memory product list that provides CRUD operations
    /// and utility methods for displaying and searching products.
    /// </summary>
    public class ProductList : ListData<IProduct>
    {
        /// <summary>
        /// Displays all products about it's stock.
        /// </summary>
        public void DisplayProducts(bool availableOnly = true)
        {
            if (availableOnly)
            {
                var availableProducts = Items.Where(p => p.Stock > 0).ToList();

                if (availableProducts.Count == 0)
                {
                    Console.WriteLine("❌ No products available in stock.");
                    return;
                }

                Console.WriteLine("📦 Available Products:");
                Console.WriteLine(new string('-', 40));

                foreach (var product in availableProducts)
                    Console.WriteLine($"🛒 {product}");

                Console.WriteLine(new string('-', 40));

                Console.WriteLine($"Total Products Available: {availableProducts.Count}");
            }
            else
            {
                // unavailable products
                var unavailableProducts = Items.Where(p => p.Stock == 0).ToList();
                if (unavailableProducts.Count > 0)
                {
                    Console.WriteLine("\n⚠️ Unavailable Products (Out of Stock):");
                    Console.WriteLine(new string('-', 40));

                    foreach (var product in unavailableProducts)
                        Console.WriteLine($"❌ {product}");

                    Console.WriteLine(new string('-', 40));
                    Console.WriteLine($"Total Unavailable Products: {unavailableProducts.Count}");
                }
            }

            Console.WriteLine("✅ End of product list.");
        }

        /// <summary>
        /// Finds a product by its unique identifier.
        /// </summary>
        /// <param name="id">The product ID to search for.</param>
        /// <returns>The matching product, or <c>null</c> if not found.</returns>
        public Task<IProduct?> FindByIdAsync(int id)
        {
            var product = Items.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }

        /// <summary>
        /// Finds a product by its name (case-insensitive).
        /// </summary>
        /// <param name="name">The name of the product to search for.</param>
        /// <returns>The matching product, or <c>null</c> if not found.</returns>
        public Task<IProduct?> FindByNameAsync(string name)
        {
            var product = Items.FirstOrDefault(p =>
                string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(product);
        }

        /// <summary>
        /// Finds all products within the specified price range (inclusive).
        /// </summary>
        /// <param name="minPrice">The minimum price (inclusive).</param>
        /// <param name="maxPrice">The maximum price (inclusive).</param>
        /// <returns>A collection of matching products.</returns>
        public Task<IEnumerable<IProduct>> FindByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var products = Items.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
            return Task.FromResult(products);
        }

        /// <summary>
        /// Finds all products currently available (i.e., with stock > 0).
        /// </summary>
        /// <returns>A collection of available products.</returns>
        public Task<IEnumerable<IProduct>> GetAvailableProductsAsync()
        {
            var available = Items.Where(p => p.IsAvailable());
            return Task.FromResult(available);
        }
    }
}
