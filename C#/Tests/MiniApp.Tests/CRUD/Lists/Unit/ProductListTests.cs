// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : [francoandreDev 🧑‍💻]
// Created          : 2025-11-03
// Description      : Unit tests for ProductList CRUD and query methods.
// ***********************************************************************

using MiniApp.CRUD.Lists.ProductList;
using MiniApp.Models.Products;

namespace MiniApp.Tests.CRUD.Lists.Unit
{
    /// <summary>
    /// 🧪 Unit tests for <see cref="ProductList"/> CRUD operations and query methods.
    /// Verifies creation, search, and filtering of <see cref="Product"/> entities.
    /// </summary>
    public partial class ProductListTests
    {
        #region 🧰 Fields & Setup

        private readonly ProductList _productList;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductListTests"/> class.
        /// Sets up a fresh <see cref="ProductList"/> for each test.
        /// </summary>
        public ProductListTests()
        {
            _productList = new();
        }

        #endregion

        #region 🧩 CREATE & READ

        /// <summary>
        /// ✅ Ensures that <see cref="ProductList.CreateAsync"/> adds products correctly
        /// and <see cref="ProductList.ReadAllAsync"/> returns them.
        /// </summary>
        [Fact]
        public async Task CreateAndReadProducts_ShouldWorkCorrectly()
        {
            // Arrange
            var product1 = new Product(1, "Laptop", 999.99m, 5);
            var product2 = new Product(2, "Mouse", 25.50m, 0); // out of stock

            // Act
            await _productList.CreateAsync(product1);
            await _productList.CreateAsync(product2);

            var allProducts = await _productList.ReadAllAsync();

            // Assert
            Assert.Equal(2, allProducts.Count());
            Assert.Contains(allProducts, p => p.Name == "Laptop");
            Assert.Contains(allProducts, p => p.Name == "Mouse");
        }

        #endregion

        #region 🔍 FIND

        /// <summary>
        /// 🔎 Tests that <see cref="ProductList.FindByIdAsync"/> and
        /// <see cref="ProductList.FindByNameAsync"/> return the correct product.
        /// </summary>
        [Fact]
        public async Task FindByIdAndName_ShouldReturnCorrectProduct()
        {
            // Arrange
            var product = new Product(3, "Keyboard", 45m, 10);
            await _productList.CreateAsync(product);

            // Act
            var byId = await _productList.FindByIdAsync(3);
            var byName = await _productList.FindByNameAsync("keyboard"); // case-insensitive

            // Assert
            Assert.NotNull(byId);
            Assert.Equal("Keyboard", byId!.Name);
            Assert.NotNull(byName);
            Assert.Equal(3, byName!.Id);
        }

        /// <summary>
        /// 💰 Verifies that <see cref="ProductList.FindByPriceRangeAsync"/> returns products
        /// whose price is within the specified range.
        /// </summary>
        [Fact]
        public async Task FindByPriceRange_ShouldReturnMatchingProducts()
        {
            // Arrange
            var p1 = new Product(4, "Monitor", 150m, 3);
            var p2 = new Product(5, "USB Cable", 10m, 20);
            await _productList.CreateAsync(p1);
            await _productList.CreateAsync(p2);

            // Act
            var result = await _productList.FindByPriceRangeAsync(50, 200);

            // Assert
            Assert.Single(result);
            Assert.Equal("Monitor", result.First().Name);
        }

        #endregion

        #region 📦 AVAILABILITY

        /// <summary>
        /// 🧾 Ensures that <see cref="ProductList.GetAvailableProductsAsync"/> returns only products with stock greater than zero.
        /// </summary>
        [Fact]
        public async Task GetAvailableProducts_ShouldReturnOnlyProductsWithStock()
        {
            // Arrange
            var p1 = new Product(6, "Webcam", 60m, 0); // out of stock
            var p2 = new Product(7, "Headset", 80m, 5);
            await _productList.CreateAsync(p1);
            await _productList.CreateAsync(p2);

            // Act
            var available = await _productList.GetAvailableProductsAsync();

            // Assert
            Assert.Single(available);
            Assert.Equal("Headset", available.First().Name);
        }

        #endregion
    }
}
