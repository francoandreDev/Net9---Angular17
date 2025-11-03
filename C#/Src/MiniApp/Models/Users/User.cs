namespace MiniApp.Models.Users
{
    /// <summary>
    /// Represents a user with an identifier, username, and email address.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="username">The username of the user.</param>
    /// <param name="email">The email address of the user.</param>
    public class User(int id, string username, string email) : IUser
    {
        /// <inheritdoc/>
        public int Id { get; init; } = id;

        /// <inheritdoc/>
        public string Username { get; init; } = username;

        /// <inheritdoc/>
        public string Email { get; init; } = email;

        /// <summary>
        /// Determines whether the user data is valid.
        /// </summary>
        /// <returns>
        /// <c>true</c> if <see cref="Id"/> is greater than zero,
        /// <see cref="Username"/> is not empty or whitespace,
        /// <see cref="Email"/> is not empty and contains an '@' symbol; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            return Id > 0
                && !string.IsNullOrWhiteSpace(Username)
                && !string.IsNullOrWhiteSpace(Email)
                && Email.Contains('@');
        }

        /// <summary>
        /// Returns a human-readable string representation of the user.
        /// </summary>
        /// <returns>A string containing the username and email in the format "Username (Email)".</returns>
        public override string ToString() => $"{Username} ({Email})";
    }
}
