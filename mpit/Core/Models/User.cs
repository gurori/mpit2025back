namespace mpit.Core.Models;

public sealed class User
{
    public User() { }

    public Guid Id { get; private set; }
    public string PasswordHash { get; private set; }
    public string FirstName { get; private set; }
    public string Role { get; private set; }
    public string Email { get; private set; }
    public int Coins { get; private set; }
}
