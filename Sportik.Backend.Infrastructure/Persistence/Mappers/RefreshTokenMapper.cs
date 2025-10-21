using Sportik.Backend.Domain.Models;
using Sportik.Backend.Infrastructure.Persistence.Entities;

namespace Sportik.Backend.Infrastructure.Persistence.Mappers;

internal static class RefreshTokenMapper
{
    public static UserRefreshToken ToEntity(RefreshToken refreshToken, string hash)
    {
        return new UserRefreshToken
        {
            Id = refreshToken.Id,
            UserId = refreshToken.UserId,
            Hash = hash,
            ExpiresAt = refreshToken.ExpiresAt,
            CreatedAt = refreshToken.CreatedAt,
            RevokedAt = refreshToken.RevokedAt,
        };
    }

    public static RefreshToken ToDomain(UserRefreshToken refreshToken, string token)
    {
        return new RefreshToken
        {
            Id = refreshToken.Id,
            UserId = refreshToken.UserId,
            Token = token,
            ExpiresAt = refreshToken.ExpiresAt,
            CreatedAt = refreshToken.CreatedAt,
            RevokedAt = refreshToken.RevokedAt,
        };
    }
}