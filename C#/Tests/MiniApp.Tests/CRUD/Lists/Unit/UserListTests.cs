// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : [francoandreDev 🧑‍💻]
// Created          : 2025-11-03
// Description      : Unit tests for UserList CRUD and validation methods.
// ***********************************************************************

using MiniApp.CRUD.Lists.UserList;
using MiniApp.Models.Users;

namespace MiniApp.Tests.CRUD.Lists.Unit
{
    /// <summary>
    /// 🧪 Unit tests for <see cref="UserList"/> CRUD operations and validation methods.
    /// Ensures that create, read, update, delete, and validation logic work as expected.
    /// </summary>
    public class UserListTests
    {
        #region 🧰 Fields & Setup

        private readonly UserList _userList;

        /// <summary>
        /// Initializes a new instance of <see cref="UserListTests"/>.
        /// </summary>
        public UserListTests()
        {
            _userList = new UserList();
        }

        #endregion

        #region 🧩 CREATE

        /// <summary>
        /// ✅ Verifies that <see cref="UserList.CreateAsync"/> successfully adds a new user to the list.
        /// </summary>
        [Fact]
        public async Task CreateAsync_ShouldAddUser()
        {
            // Arrange
            var user = new User(1, "Alice", "alice@mail.com");

            // Act
            await _userList.CreateAsync(user);
            var all = await _userList.ReadAllAsync();

            // Assert
            Assert.Single(all);
            Assert.Contains(user, all);
        }

        /// <summary>
        /// 🚫 Ensures that <see cref="UserList.TryAddUserAsync"/> prevents adding users with duplicate emails.
        /// </summary>
        [Fact]
        public async Task TryAddUserAsync_ShouldPreventDuplicateEmail()
        {
            // Arrange
            var user1 = new User(1, "Alice", "alice@mail.com");
            var user2 = new User(2, "Bob", "alice@mail.com");

            // Act
            await _userList.TryAddUserAsync(user1);
            var added = await _userList.TryAddUserAsync(user2);

            // Assert
            Assert.False(added);
            var all = await _userList.ReadAllAsync();
            Assert.Single(all);
        }

        #endregion

        #region 🔍 READ / FIND

        /// <summary>
        /// 🔎 Tests that <see cref="UserList.FindByIdAsync"/> returns the correct user by ID.
        /// </summary>
        [Fact]
        public async Task FindByIdAsync_ShouldReturnCorrectUser()
        {
            // Arrange
            var user = new User(1, "Alice", "alice@mail.com");
            await _userList.CreateAsync(user);

            // Act
            var found = await _userList.FindByIdAsync(1);

            // Assert
            Assert.NotNull(found);
            Assert.Equal("Alice", found!.Username);
        }

        /// <summary>
        /// 🕳️ Verifies that <see cref="UserList.FindByEmailAsync"/> returns <c>null</c> if no user is found.
        /// </summary>
        [Fact]
        public async Task FindByEmailAsync_ShouldReturnNull_WhenNotFound()
        {
            // Act
            var found = await _userList.FindByEmailAsync("missing@mail.com");

            // Assert
            Assert.Null(found);
        }

        #endregion

        #region ⚙️ UPDATE

        /// <summary>
        /// ♻️ Tests that <see cref="UserList.UpdateAsync"/> correctly replaces a user at a specific index.
        /// </summary>
        [Fact]
        public async Task UpdateAsync_ShouldReplaceUserAtGivenIndex()
        {
            // Arrange
            var user1 = new User(1, "Alice", "alice@mail.com");
            var user2 = new User(2, "Bob", "bob@mail.com");

            await _userList.CreateAsync(user1);

            // Act
            await _userList.UpdateAsync(0, user2);
            var all = await _userList.ReadAllAsync();

            // Assert
            Assert.Single(all);
            Assert.Equal("Bob", all.First().Username);
        }

        #endregion

        #region 🗑️ DELETE

        /// <summary>
        /// 🧹 Ensures that <see cref="UserList.DeleteAsync"/> removes the user at the given index.
        /// </summary>
        [Fact]
        public async Task DeleteAsync_ShouldRemoveUserByIndex()
        {
            // Arrange
            var user = new User(1, "Alice", "alice@mail.com");
            await _userList.CreateAsync(user);

            // Act
            await _userList.DeleteAsync(0);
            var all = await _userList.ReadAllAsync();

            // Assert
            Assert.Empty(all);
        }

        /// <summary>
        /// 🧽 Confirms that <see cref="UserList.RemoveByIdAsync"/> deletes the correct user by ID.
        /// </summary>
        [Fact]
        public async Task RemoveByIdAsync_ShouldRemoveCorrectUser()
        {
            // Arrange
            var user1 = new User(1, "Alice", "alice@mail.com");
            var user2 = new User(2, "Bob", "bob@mail.com");

            await _userList.CreateAsync(user1);
            await _userList.CreateAsync(user2);

            // Act
            var result = await _userList.RemoveByIdAsync(1);

            // Assert
            Assert.True(result);
            var all = await _userList.ReadAllAsync();
            Assert.Single(all);
            Assert.DoesNotContain(all, u => u.Id == 1);
        }

        #endregion

        #region 🧠 VALIDATION

        /// <summary>
        /// 🚫 Verifies that <see cref="UserList.CanAddUserAsync"/> returns <c>false</c> for invalid users.
        /// </summary>
        [Fact]
        public async Task CanAddUserAsync_ShouldReturnFalse_ForInvalidUser()
        {
            // Arrange
            var invalid = new User(0, "", "invalid");

            // Act
            var result = await _userList.CanAddUserAsync(invalid);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// 🔤 Tests that <see cref="UserList.UsernameExistsAsync"/> correctly identifies existing usernames.
        /// </summary>
        [Fact]
        public async Task UsernameExistsAsync_ShouldReturnTrue_WhenExists()
        {
            // Arrange
            await _userList.CreateAsync(new User(1, "Alice", "alice@mail.com"));

            // Act
            var exists = await _userList.UsernameExistsAsync("Alice");

            // Assert
            Assert.True(exists);
        }

        #endregion
    }
}
