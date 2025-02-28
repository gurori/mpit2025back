
namespace mpit.DataAccess.Entities;

public sealed class UserEntity : BaseEntity {

    public string PasswordHash {get; set;} = string.Empty;
    public string Role {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string PhoneNumber {get; set;} = string.Empty; 
}