// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Negative tests for <see cref="ProductList"/> CRUD operations.
// ***********************************************************************

using MiniApp.CRUD.Lists.ProductList;
using MiniApp.Models.Products;

namespace MiniApp.Tests.CRUD.Lists.Negative
{
    /// <summary>
    /// Negative tests for <see cref="ProductList"/>.
    /// </summary>
    public class ProductListTests
    {
        private readonly ProductList _productList;

        #region Setup

        /// <summary>
        /// Initializes a new instance of <see cref="ProductListTests"/>.
        /// </summary>
        public ProductListTests()
        {
            _productList = new ProductList();
        }

        #endregion

        #region CREATE

        /// <summary>
        /// Creating a null product should throw ArgumentNullException.
        /// </summary>
        [Fact]
        public async Task CreateProduct_WithNull_ShouldThrow()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _productList.CreateAsync(null!));
        }

        #endregion

        #region READ

        /// <summary>
        /// Finding a product by a non-existent ID should return null.
        /// </summary>
        [Fact]
        public async Task FindById_NonExistent_ShouldReturnNull()
        {
            var product = await _productList.FindByIdAsync(999);
            Assert.Null(product);
        }

        /// <summary>
        /// Finding a product by a non-existent name should return null.
        /// </summary>
        [Fact]
        public async Task FindByName_NonExistent_ShouldReturnNull()
        {
            var product = await _productList.FindByNameAsync("NonExistentProduct");
            Assert.Null(product);
        }

        #endregion

        #region UPDATE

        /// <summary>
        /// Updating a product at a non-existent index should throw IndexOutOfRangeException.
        /// </summary>
        [Fact]
        public async Task Update_NonExistentIndex_ShouldThrow()
        {
            var product = new Product(2, "Mouse", 25.50m, 10);
            await _productList.CreateAsync(product);

            var updatedProduct = new Product(2, "Mouse Pro", 30m, 5);

            await Assert.ThrowsAsync<IndexOutOfRangeException>(() => _productList.UpdateAsync(5, updatedProduct));
        }

        /// <summary>
        /// Updating a product with null should throw ArgumentNullException.
        /// </summary>
        [Fact]
        public async Task Update_WithNull_ShouldThrow()
        {
            var product = new Product(3, "Keyboard", 45m, 10);
            await _productList.CreateAsync(product);

            await Assert.ThrowsAsync<ArgumentNullException>(() => _productList.UpdateAsync(0, null!));
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Deleting a product at a non-existent index should throw IndexOutOfRangeException.
        /// </summary>
        [Fact]
        public async Task Delete_NonExistentIndex_ShouldThrow()
        {
            var product = new Product(1, "Laptop", 999.99m, 5);
            await _productList.CreateAsync(product);

            await Assert.ThrowsAsync<IndexOutOfRangeException>(() => _productList.DeleteAsync(10));
        }

        #endregion
    }
}
