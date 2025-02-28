using Microsoft.AspNetCore.Mvc;

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

        string passwordHash = _passwordHasher.Generate(request.Password);

        await _repository.CreateAsync(request.Login, passwordHash);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest request) {
        await Task.CompletedTask;
        return Ok();
    }
}