namespace mpit.Core.DTOs;

public sealed record RegisterUserRequest(
    string Login,
    string Password
);