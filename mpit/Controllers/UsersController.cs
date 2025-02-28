using Microsoft.AspNetCore.Mvc;
using mpit.Application.Auth;
using mpit.Core.DTOs;
using mpit.DataAccess.Repositories;

namespace mpit.controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController(
    UsersRepository repository,
    PasswordHasher passwordHasher
) : ControllerBase {

    private readonly UsersRepository _repository = repository;

    private readonly PasswordHasher _passwordHasher = passwordHasher;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request) {

        string passwordHash = _passwordHasher
            .Generate(request.Password);

        bool isUserAlreadyExist = await _repository
            .TryCreateAsync(request.Login, passwordHash);

        if (isUserAlreadyExist) {
            return Problem(statusCode: 409, 
                detail: "Данный пользователь уже зарегестрирован");
        }

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest request) {
        await Task.CompletedTask;
        return Ok();
    }
}