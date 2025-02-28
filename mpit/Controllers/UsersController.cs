using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mpit.Application.Auth;
using mpit.Core.DTOs;
using mpit.Core.Models;
using mpit.DataAccess.Repositories;

namespace mpit.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController(
    UsersRepository repository,
    IMapper mapper,
    PasswordHasher passwordHasher,
    JwtProvider jwtProvider
) : BaseController
{
    private readonly UsersRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly PasswordHasher _passwordHasher = passwordHasher;
    private readonly JwtProvider _jwtProvider = jwtProvider;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        string passwordHash = _passwordHasher.Generate(request.Password);

        bool isUserAlreadyExist = await _repository.TryCreateAsync(request.Login, passwordHash);

        if (isUserAlreadyExist)
        {
            return Problem(statusCode: 409, detail: "Данный пользователь уже зарегестрирован");
        }

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest request)
    {
        var userEntity = await _repository.GetByLoginAsync(request.Login);
        if (userEntity is null)
            return Problem(statusCode: 404, detail: "Данный пользователь не зарегестрирован");

        if (!_passwordHasher.Verify(request.Password, userEntity.PasswordHash))
            Problem(statusCode: 409, detail: "Неверный пароль");

        var user = _mapper.Map<User>(userEntity);
        var token = await _jwtProvider.GenerateTokenAsync(user);

        return Ok(token);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var token = GetTokenFromHeaders();

        var tokenValidationResult = await _jwtProvider.ValidateTokenAsync(token);

        if (!tokenValidationResult.IsValid)
            return Unauthorized("Token is not valid");

        string? id = tokenValidationResult.Claims["id"].ToString();

        if (id is null)
            return BadRequest("Id does not found in token");

        var user = await _repository.GetByIdAsync(Guid.Parse(id));
        if (user is null)
            return Unauthorized("User does not found");
        return Ok(user);
    }
}
