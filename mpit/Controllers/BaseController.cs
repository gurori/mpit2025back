using Microsoft.AspNetCore.Mvc;

public abstract class BaseController : ControllerBase
{
    protected string GetTokenFromHeaders() =>
        Request.Headers.Authorization.FirstOrDefault()!.Split(" ").Last();
}
