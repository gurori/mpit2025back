namespace mpit.DataAccess.Entities;

public sealed class UserEntity : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public ICollection<Post> Posts { get; set; } = [];
}
