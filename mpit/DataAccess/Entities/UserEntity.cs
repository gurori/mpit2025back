public sealed class UserEntity : BaseEntity {

    public string PasswordHash {get; set;} = string.Empty;
    public string Role {get; set;} = string.Empty;
}