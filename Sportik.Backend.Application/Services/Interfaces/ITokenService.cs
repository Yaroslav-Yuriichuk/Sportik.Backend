using Sportik.Backend.Domain.Entities;

namespace Sportik.Backend.Application.Services.Interfaces;

public interface ITokenService
{
    AccessToken GenerateAccessToken(Guid userId, string email);

    RefreshToken GenerateRefreshToken(Guid userId);

    string HashToken(string token);
}