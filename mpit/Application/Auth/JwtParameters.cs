using System.Text;
using Microsoft.IdentityModel.Tokens;

public static class JwtParameters
{
    public static TokenValidationParameters GetTokenValidationParameters(JwtOptions jwtOptions) =>
        new()
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions!.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtOptions!.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions!.SecretKey)
            ),
        };
}
