// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Integration tests for ProductList CRUD operations
// ***********************************************************************

using MiniApp.CRUD.Lists.ProductList;
using MiniApp.Models.Products;

namespace MiniApp.Tests.CRUD.Lists.Integration
{
    /// <summary>
    /// 🔗 Integration tests for <see cref="ProductList"/>.
    /// Validates full CRUD behavior, stock filtering, and search capabilities.
    /// </summary>
    public class ProductListTests
    {
        private readonly ProductList _productList;

        /// <summary>
        /// Initializes a new instance of <see cref="ProductListTests"/>.
        /// </summary>
        public ProductListTests()
        {
            _productList = new ProductList();
        }

        #region 🔄 Full CRUD Flow

        /// <summary>
        /// ✅ Ensures that the full CRUD flow (Create → Read → Update → Delete)
        /// works correctly and maintains data consistency.
        /// </summary>
        [Fact]
        public async Task FullCrudFlow_ShouldWorkCorrectly()
        {
            // --- CREATE ---
            var p1 = new Product(1, "Laptop", 1200m, 5);
            var p2 = new Product(2, "Mouse", 25.50m, 0); // Out of stock
            var p3 = new Product(3, "Keyboard", 45m, 10);

            await _productList.CreateAsync(p1);
            await _productList.CreateAsync(p2);
            await _productList.CreateAsync(p3);

            Assert.Equal(3, _productList.Count);

            // --- READ ---
            var allProducts = await _productList.ReadAllAsync();
            Assert.Contains(allProducts, p => p.Name == "Laptop");
            Assert.Contains(allProducts, p => p.Name == "Mouse");
            Assert.Contains(allProducts, p => p.Name == "Keyboard");

            // --- AVAILABLE PRODUCTS ---
            var available = await _productList.GetAvailableProductsAsync();
            Assert.DoesNotContain(available, p => p.Stock == 0);
            Assert.Equal(2, available.Count()); // Laptop + Keyboard

            // --- UPDATE ---
            var updatedP1 = new Product(1, "Laptop Pro", 1500m, 3);
            await _productList.UpdateAsync(0, updatedP1);

            var fetchedP1 = await _productList.FindByIdAsync(1);
            Assert.Equal("Laptop Pro", fetchedP1!.Name);
            Assert.Equal(1500m, fetchedP1.Price);

            // --- DELETE ---
            await _productList.DeleteAsync(1); // Remove Mouse
            Assert.Equal(2, _productList.Count);
            var deletedProduct = await _productList.FindByIdAsync(2);
            Assert.Null(deletedProduct);
        }

        #endregion

        #region 🔍 Search by Name

        /// <summary>
        /// 🧭 Tests the <see cref="ProductList.FindByNameAsync"/> method
        /// to ensure product search by name is case-insensitive and returns the correct item.
        /// </summary>
        [Fact]
        public async Task SearchByName_ShouldReturnCorrectProduct()
        {
            var p = new Product(4, "Monitor", 300m, 8);
            await _productList.CreateAsync(p);

            var result = await _productList.FindByNameAsync("monitor");
            Assert.NotNull(result);
            Assert.Equal(4, result!.Id);
        }

        #endregion

        #region 💰 Price Range Filter

        /// <summary>
        /// 💵 Ensures that <see cref="ProductList.FindByPriceRangeAsync"/>
        /// correctly filters products within a given price range.
        /// </summary>
        [Fact]
        public async Task FindByPriceRange_ShouldReturnCorrectProducts()
        {
            var products = await _productList.FindByPriceRangeAsync(40m, 1300m);
            Assert.All(products, p => Assert.InRange(p.Price, 40m, 1300m));
        }

        #endregion
    }
}
