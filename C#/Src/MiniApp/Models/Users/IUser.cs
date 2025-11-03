namespace MiniApp.Models.Users
{
    /// <summary>
    /// Defines a contract for user entities, including identification,
    /// credentials, and validation logic.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Gets the unique identifier of the user.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the username used to identify the user.
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Gets the user's email address.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Determines whether the user entity contains valid data.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the user has a valid ID, username, and email; otherwise, <c>false</c>.
        /// </returns>
        bool IsValid();
    }
}
