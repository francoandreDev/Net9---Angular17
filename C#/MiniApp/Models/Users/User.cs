namespace MiniApp.Models.Users
{
    public class User(int id, string username, string email) : IUser
    {
        public int Id { get; init; } = id;
        public string Username { get; init; } = username;
        public string Email { get; init; } = email;

        public bool IsValid()
        {
            return Id > 0
                && !string.IsNullOrWhiteSpace(Username)
                && !string.IsNullOrWhiteSpace(Email)
                && Email.Contains('@');
        }

        public override string ToString() => $"{Username} ({Email})";
    }
}
