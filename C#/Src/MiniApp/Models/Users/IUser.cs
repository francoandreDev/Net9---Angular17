namespace MiniApp.Models.Users
{
    public interface IUser
    {
        int Id { get; }
        string Username { get; }
        string Email { get; }

        bool IsValid();
    }
}
