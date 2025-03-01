using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using mpit.Core.Models;

public class JwtProvider(IOptions<JwtOptions> options, PermissionsRepository permissionsRepository)
{
    private readonly PermissionsRepository _permissionsRepo = permissionsRepository;
    private readonly JwtSecurityTokenHandler _tokenHandler = new();
    private readonly JwtOptions _options = options.Value;

    public async Task<string> GenerateTokenAsync(User user)
    {
        List<Claim> claims = [new("id", user.Id.ToString())];

        HashSet<string> permissions = await _permissionsRepo.GetPermissionsAsync(user.Role);

        foreach (string permission in permissions)
            claims.Add(new("p", permission));

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddDays(_options.ExpiresDays)
        );

        return _tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenValidationParameters = JwtParameters.GetTokenValidationParameters(_options);

        return _tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
    }

    public async Task<TokenValidationResult> ValidateTokenAsync(string token)
    {
        var tokenValidationParameters = JwtParameters.GetTokenValidationParameters(_options);

        return await _tokenHandler.ValidateTokenAsync(token, tokenValidationParameters);
    }
}
