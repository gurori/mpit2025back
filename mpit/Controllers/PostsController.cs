using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public sealed class PostsController(PostsRepository repository, JwtProvider jwtProvider)
    : BaseController
{
    private readonly PostsRepository _repository = repository;
    private readonly JwtProvider _jwtProvider = jwtProvider;

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(PostRequest post)
    {
        var token = GetTokenFromHeaders();

        var tokenValidationResult = await _jwtProvider.ValidateTokenAsync(token);

        if (!tokenValidationResult.IsValid)
            return Unauthorized("Token is not valid");

        string? id = tokenValidationResult.Claims["id"].ToString();

        if (id is null)
            return BadRequest("Id does not found in token");

        await _repository.CreateAsync(Guid.Parse(id), post);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _repository.GetAll());
    }
}
