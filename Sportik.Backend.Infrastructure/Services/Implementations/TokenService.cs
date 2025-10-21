using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Models;

namespace Sportik.Backend.Infrastructure.Services.Implementations;

internal sealed class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AccessToken GenerateAccessToken(Guid userId, string email)
    {
        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Email, email)
        };

        DateTimeOffset now = DateTimeOffset.UtcNow;

        int minutes = _configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes");
        DateTimeOffset expiresAt = now.AddMinutes(minutes);

        SecurityToken securityToken = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiresAt.UtcDateTime,
            signingCredentials: credentials);

        return new AccessToken
        {
            UserId = userId,
            Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
            ExpiresAt = expiresAt,
        };
    }

    public RefreshToken GenerateRefreshToken(Guid userId)
    {
        byte[] randomNumber = new byte[64];

        using RandomNumberGenerator generator = RandomNumberGenerator.Create();
        generator.GetBytes(randomNumber);

        DateTimeOffset now = DateTimeOffset.UtcNow;

        int days = _configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays");
        DateTimeOffset expiresAt = now.AddDays(days);

        return new RefreshToken
        {
            UserId = userId,
            Token = Convert.ToBase64String(randomNumber),
            ExpiresAt = expiresAt,
            CreatedAt = now,
        };
    }

    public string HashToken(string token)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));

        return Convert.ToBase64String(bytes);
    }
}