namespace mpit.Core.DTOs;

public sealed record RegisterUserRequest(
    string Login,
    string FirstName,
    string Password,
    string Role
);
