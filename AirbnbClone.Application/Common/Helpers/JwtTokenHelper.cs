using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AirbnbClone.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AirbnbClone.Application.Common.Helpers;

public class JwtTokenHelper
{
    public static string GenerateJwtAccessToken(IConfiguration configuration, User user)
    {
        var expiresMinutesStr = configuration["Jwt:ExpiresMinutes"];
        var expiresMinutes = 10080;
        if (int.TryParse(expiresMinutesStr, out var parsed) && parsed > 0)
        {
            expiresMinutes = parsed;
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.FirstName),
            new(ClaimTypes.Email, user.EmailAddress),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.EmailAddress),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}