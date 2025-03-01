using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public sealed class PostsController(PostsRepository repository) : BaseController
{
    private readonly PostsRepository _repository = repository;
}
