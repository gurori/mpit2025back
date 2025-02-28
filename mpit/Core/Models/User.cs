namespace mpit.Core.Models;

public sealed record User(
    Guid Id,
    string PasswordHash,
    string Role,
    string Email,
    string PhoneNumber
);