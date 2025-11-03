// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Integration tests for UserList CRUD operations
// ***********************************************************************

using MiniApp.CRUD.Lists.UserList;
using MiniApp.Models.Users;

namespace MiniApp.Tests.CRUD.Lists.Integration
{
    /// <summary>
    /// 🔗 Integration tests for <see cref="UserList"/>.
    /// Verifies sequential CRUD operations (Create → Read → Update → Delete)
    /// and ensures correct data consistency and lookup behavior.
    /// </summary>
    public class UserListTests
    {
        private readonly UserList _userList;

        /// <summary>
        /// Initializes a new instance of <see cref="UserListTests"/>.
        /// </summary>
        public UserListTests()
        {
            _userList = new UserList();
        }

        #region 🔄 Full CRUD Flow

        /// <summary>
        /// ✅ Ensures that the full CRUD flow (Create → Read → Update → Delete)
        /// operates correctly on <see cref="UserList"/>.
        /// </summary>
        [Fact]
        public async Task FullCrudFlow_ShouldWorkCorrectly()
        {
            // --- CREATE ---
            var user1 = new User(1, "Alice", "alice@mail.com");
            var user2 = new User(2, "Bob", "bob@mail.com");

            await _userList.CreateAsync(user1);
            await _userList.CreateAsync(user2);

            var allUsers = await _userList.ReadAllAsync();
            Assert.Equal(2, allUsers.Count());

            // --- READ ---
            var findAlice = await _userList.FindByUsernameAsync("Alice");
            Assert.NotNull(findAlice);
            Assert.Equal("alice@mail.com", findAlice!.Email);

            var findBobById = await _userList.FindByIdAsync(2);
            Assert.NotNull(findBobById);
            Assert.Equal("Bob", findBobById!.Username);

            // --- UPDATE ---
            var updatedBob = new User(2, "Robert", "bob@mail.com");
            await _userList.UpdateAsync(1, updatedBob); // index 1 = Bob
            var checkBob = await _userList.FindByIdAsync(2);
            Assert.Equal("Robert", checkBob!.Username);

            // --- DELETE ---
            await _userList.DeleteAsync(0); // remove Alice
            var remainingUsers = await _userList.ReadAllAsync();
            Assert.Single(remainingUsers);
            Assert.Equal("Robert", remainingUsers.First().Username);
        }

        #endregion

        #region ⚙️ Update & Count Validation

        /// <summary>
        /// 🧩 Verifies that Create, Update, and Delete operations
        /// maintain consistent count tracking in <see cref="UserList"/>.
        /// </summary>
        [Fact]
        public async Task Integration_AddUpdateDelete_CheckCountConsistency()
        {
            // Arrange
            var user = new User(10, "Charlie", "charlie@mail.com");

            // Act
            await _userList.CreateAsync(user);
            Assert.Equal(1, _userList.Count);

            await _userList.UpdateAsync(0, new User(10, "Charles", "charlie@mail.com"));
            var updatedUser = await _userList.FindByIdAsync(10);
            Assert.Equal("Charles", updatedUser!.Username);

            await _userList.DeleteAsync(0);
            Assert.Equal(0, _userList.Count);
        }

        #endregion

        #region 🔍 Lookup Tests

        /// <summary>
        /// 🕵️‍♂️ Ensures that user lookup methods
        /// (<see cref="UserList.FindByEmailAsync"/> and <see cref="UserList.FindByUsernameAsync"/>)
        /// return the correct results.
        /// </summary>
        [Fact]
        public async Task Integration_FindByEmailAndUsername_ShouldReturnCorrectResults()
        {
            // Arrange
            var user1 = new User(101, "Dana", "dana@mail.com");
            var user2 = new User(102, "Eve", "eve@mail.com");

            await _userList.CreateAsync(user1);
            await _userList.CreateAsync(user2);

            // Act & Assert
            var findByEmail = await _userList.FindByEmailAsync("eve@mail.com");
            Assert.NotNull(findByEmail);
            Assert.Equal(102, findByEmail!.Id);

            var findByUsername = await _userList.FindByUsernameAsync("Dana");
            Assert.NotNull(findByUsername);
            Assert.Equal(101, findByUsername!.Id);
        }

        #endregion
    }
}
