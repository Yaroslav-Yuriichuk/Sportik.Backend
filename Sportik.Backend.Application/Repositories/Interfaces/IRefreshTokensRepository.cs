using Sportik.Backend.Domain.Entities;

namespace Sportik.Backend.Application.Repositories.Interfaces;

public interface IRefreshTokensRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);

    Task<RefreshToken> AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);

    Task<RefreshToken?> RevokeAsync(string token, CancellationToken cancellationToken = default);

    Task<RefreshToken> ReplaceAsync(string oldToken, RefreshToken newRefreshToken, CancellationToken cancellationToken = default);
}