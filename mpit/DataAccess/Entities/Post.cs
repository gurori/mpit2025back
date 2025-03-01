using mpit.DataAccess.Entities;

public sealed class Post : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }
    public UserEntity Author { get; set; }
}
