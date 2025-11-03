// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Unit tests for the Product model to validate its
//                    constructor, logic, and string formatting behavior.
// ***********************************************************************

using MiniApp.Models.Products;

namespace MiniApp.Tests.Models.Products
{
    /// <summary>
    /// 🛒 Provides unit tests for the <see cref="Product"/> model,
    /// verifying its initialization, availability logic, and output formatting.
    /// </summary>
    public class ProductTests
    {
        // ===========================================================
        #region 🧱 Constructor Tests
        // ===========================================================

        /// <summary>
        /// 🧩 Ensures the <see cref="Product"/> constructor correctly initializes
        /// all properties with the provided values.
        /// </summary>
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            int id = 1;
            string name = "Laptop";
            decimal price = 999.99m;
            int stock = 5;

            // Act
            var product = new Product(id, name, price, stock);

            // Assert
            Assert.Equal(id, product.Id);
            Assert.Equal(name, product.Name);
            Assert.Equal(price, product.Price);
            Assert.Equal(stock, product.Stock);
        }

        #endregion

        // ===========================================================
        #region ✅ Availability Logic Tests
        // ===========================================================

        /// <summary>
        /// ✅ Verifies that <see cref="Product.IsAvailable"/> returns <c>true</c>
        /// for a valid and properly configured product.
        /// </summary>
        [Fact]
        public void IsAvailable_ShouldReturnTrue_WhenValid()
        {
            // Arrange
            var product = new Product(1, "Phone", 499.99m, 10);

            // Act
            bool result = product.IsAvailable();

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// 🚫 Ensures that <see cref="Product.IsAvailable"/> returns <c>false</c>
        /// when any of the product’s essential properties are invalid.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <param name="name">The product name.</param>
        /// <param name="price">The product price.</param>
        /// <param name="stock">The product stock count.</param>
        [Theory]
        [InlineData(1, "", 10.5, 5)]          // name empty
        [InlineData(1, "  ", 10.5, 5)]        // name whitespace
        [InlineData(1, "Item", 0, 5)]         // price zero
        [InlineData(1, "Item", -10, 5)]       // price negative
        [InlineData(1, "Item", 10, 0)]        // stock zero
        [InlineData(1, "Item", 10, -3)]       // stock negative
        public void IsAvailable_ShouldReturnFalse_WhenInvalid(int id, string name, decimal price, int stock)
        {
            // Arrange
            var product = new Product(id, name, price, stock);

            // Act
            bool result = product.IsAvailable();

            // Assert
            Assert.False(result);
        }

        #endregion

        // ===========================================================
        #region 🖋️ ToString Formatting Tests
        // ===========================================================

        /// <summary>
        /// 🧾 Validates that <see cref="Product.ToString"/> produces
        /// the correct formatted string when stock is available.
        /// </summary>
        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var product = new Product(1, "Laptop", 999.99m, 5);

            // Act
            string result = product.ToString();

            // Assert
            Assert.Equal("Laptop - $999.99 (5 in stock)", result);
        }

        /// <summary>
        /// ⚠️ Ensures that <see cref="Product.ToString"/> correctly formats
        /// the string even when stock is zero.
        /// </summary>
        [Fact]
        public void ToString_ShouldHandleZeroStock()
        {
            // Arrange
            var product = new Product(1, "Keyboard", 49.99m, 0);

            // Act
            string result = product.ToString();

            // Assert
            Assert.Equal("Keyboard - $49.99 (0 in stock)", result);
        }

        #endregion
    }
}
