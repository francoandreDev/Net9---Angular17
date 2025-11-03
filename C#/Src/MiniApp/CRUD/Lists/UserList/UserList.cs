using MiniApp.Models.Users;
using MiniApp.CRUD.Lists.Base;

namespace MiniApp.CRUD.Lists.UserList
{
    /// <summary>
    /// In-memory implementation of <see cref="ListData{IUser}"/>
    /// with extended search and validation capabilities.
    /// </summary>
    public class UserList : ListData<IUser>
    {
        #region 🔍 Find Methods

        /// <summary>
        /// Finds a user by their unique identifier.
        /// </summary>
        public Task<IUser?> FindByIdAsync(int id)
        {
            var user = Items.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }

        /// <summary>
        /// Finds a user by their username (case-insensitive).
        /// </summary>
        public Task<IUser?> FindByUsernameAsync(string username)
        {
            var user = Items.FirstOrDefault(u =>
                string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(user);
        }

        /// <summary>
        /// Finds a user by their email (case-insensitive).
        /// </summary>
        public Task<IUser?> FindByEmailAsync(string email)
        {
            var user = Items.FirstOrDefault(u =>
                string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(user);
        }

        #endregion

        #region 🧠 Validation & Helpers

        /// <summary>
        /// Determines whether a username already exists in the list.
        /// </summary>
        public Task<bool> UsernameExistsAsync(string username)
        {
            var exists = Items.Any(u =>
                string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(exists);
        }

        /// <summary>
        /// Determines whether an email address already exists in the list.
        /// </summary>
        public Task<bool> EmailExistsAsync(string email)
        {
            var exists = Items.Any(u =>
                string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(exists);
        }

        /// <summary>
        /// Checks whether the given user object is valid and unique within the list.
        /// </summary>
        public async Task<bool> CanAddUserAsync(IUser user)
        {
            if (!user.IsValid())
                return false;

            var exists = await EmailExistsAsync(user.Email) || await UsernameExistsAsync(user.Username);
            return !exists;
        }

        #endregion

        #region ⚙️ CRUD Convenience Methods

        /// <summary>
        /// Attempts to add a user, validating uniqueness and correctness before adding.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>
        /// <c>true</c> if the user was successfully added; otherwise <c>false</c>.
        /// </returns>
        public async Task<bool> TryAddUserAsync(IUser user)
        {
            if (!await CanAddUserAsync(user))
                return false;

            await CreateAsync(user);
            return true;
        }

        /// <summary>
        /// Removes a user by their unique identifier.
        /// </summary>
        public async Task<bool> RemoveByIdAsync(int id)
        {
            var index = Items.FindIndex(u => u.Id == id);
            if (index < 0)
                return false;

            await DeleteAsync(index);
            return true;
        }

        #endregion

        #region 📜 Listing Methods

        /// <summary>
        /// Returns all valid users (filtered by <see cref="IUser.IsValid"/>).
        /// </summary>
        public Task<IEnumerable<IUser>> GetValidUsersAsync()
        {
            var valid = Items.Where(u => u.IsValid());
            return Task.FromResult(valid);
        }

        /// <summary>
        /// Prints a formatted list of users to the console.
        /// </summary>
        public async Task DisplayUsersAsync()
        {
            var users = await ReadAllAsync();

            Console.WriteLine("👥 Registered Users:");
            Console.WriteLine(new string('-', 40));

            foreach (var user in users)
                Console.WriteLine($"• {user}");

            Console.WriteLine(new string('-', 40));
            Console.WriteLine($"Total users: {Items.Count}");
        }

        #endregion
    }
}
