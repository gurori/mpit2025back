namespace mpit.Core.DTOs;

public sealed record RegisterUserRequest(
    string Login,
    string Email,
    string FirstName,
    string Password,
    string Role
);
