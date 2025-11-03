// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Negative tests for <see cref="UserList"/> to ensure proper error handling and data validation.
// ***********************************************************************

using MiniApp.CRUD.Lists.UserList;
using MiniApp.Models.Users;

namespace MiniApp.Tests.CRUD.Lists.Negative
{
    /// <summary>
    /// 🛑 Negative tests for <see cref="UserList"/> CRUD operations.
    /// Ensures proper handling of invalid users, duplicate emails, and out-of-range operations.
    /// Tests negativos para <see cref="UserList"/>: usuarios inválidos, duplicados y operaciones fuera de rango.
    /// </summary>
    public class UserListTests
    {
        private readonly UserList _userList;

        #region ⚙️ Setup

        /// <summary>
        /// Initializes a new instance of <see cref="UserListTests"/>.
        /// Inicializa una nueva instancia de <see cref="UserListTests"/>.
        /// </summary>
        public UserListTests()
        {
            _userList = new UserList();
        }

        #endregion

        #region 🚫 CREATE

        /// <summary>
        /// Verifies that creating a user with invalid fields is stored but fails validation.
        /// Verifica que crear un usuario inválido se almacena pero falla la validación.
        /// </summary>
        [Fact]
        public async Task CreateAsync_ShouldAllowInvalidUserButBeDetectedLater()
        {
            var invalidUser = new User(0, "", "not-an-email");

            await _userList.CreateAsync(invalidUser);
            var all = await _userList.ReadAllAsync();

            Assert.Single(all);
            Assert.False(invalidUser.IsValid());
        }

        /// <summary>
        /// Ensures TryAddUserAsync rejects invalid users.
        /// Garantiza que TryAddUserAsync rechaza usuarios inválidos.
        /// </summary>
        [Fact]
        public async Task TryAddUserAsync_ShouldReturnFalse_WhenUserIsInvalid()
        {
            var invalidUser = new User(0, "", "");

            var result = await _userList.TryAddUserAsync(invalidUser);

            Assert.False(result);
            var all = await _userList.ReadAllAsync();
            Assert.Empty(all);
        }

        /// <summary>
        /// Ensures TryAddUserAsync rejects users with duplicate emails.
        /// Garantiza que TryAddUserAsync rechaza usuarios con emails duplicados.
        /// </summary>
        [Fact]
        public async Task TryAddUserAsync_ShouldReturnFalse_WhenDuplicateEmail()
        {
            var user1 = new User(1, "Alice", "alice@mail.com");
            var user2 = new User(2, "Bob", "alice@mail.com");

            await _userList.CreateAsync(user1);

            var result = await _userList.TryAddUserAsync(user2);

            Assert.False(result);
            var all = await _userList.ReadAllAsync();
            Assert.Single(all);
        }

        #endregion

        #region 🚫 READ

        [Fact]
        public async Task FindByIdAsync_ShouldReturnNull_ForNonExistingUser()
        {
            var result = await _userList.FindByIdAsync(999);
            Assert.Null(result);
        }

        [Fact]
        public async Task FindByEmailAsync_ShouldReturnNull_ForNonExistingEmail()
        {
            var result = await _userList.FindByEmailAsync("missing@mail.com");
            Assert.Null(result);
        }

        [Fact]
        public async Task FindByUsernameAsync_ShouldReturnNull_ForNonExistingUsername()
        {
            var result = await _userList.FindByUsernameAsync("ghost");
            Assert.Null(result);
        }

        #endregion

        #region 🚫 UPDATE

        [Fact]
        public async Task UpdateAsync_ShouldThrow_WhenIndexOutOfRange()
        {
            var user = new User(1, "Alice", "alice@mail.com");

            await Assert.ThrowsAsync<IndexOutOfRangeException>(
                async () => await _userList.UpdateAsync(0, user));
        }

        #endregion

        #region 🚫 DELETE

        [Fact]
        public async Task DeleteAsync_ShouldThrow_WhenIndexOutOfRange()
        {
            await Assert.ThrowsAsync<IndexOutOfRangeException>(
                async () => await _userList.DeleteAsync(10));
        }

        [Fact]
        public async Task RemoveByIdAsync_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            var result = await _userList.RemoveByIdAsync(99);
            Assert.False(result);
        }

        #endregion

        #region 🚫 VALIDATION

        [Fact]
        public async Task CanAddUserAsync_ShouldReturnFalse_WhenEmailAlreadyExists()
        {
            var user1 = new User(1, "Alice", "alice@mail.com");
            var user2 = new User(2, "Bob", "alice@mail.com");

            await _userList.CreateAsync(user1);

            var canAdd = await _userList.CanAddUserAsync(user2);

            Assert.False(canAdd);
        }

        [Fact]
        public async Task UsernameExistsAsync_ShouldReturnFalse_WhenEmptyList()
        {
            var exists = await _userList.UsernameExistsAsync("ghost");
            Assert.False(exists);
        }

        #endregion
    }
}
