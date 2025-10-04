using Sportik.Backend.Domain.Entities;

namespace Sportik.Backend.Application.Repositories.Interfaces;

public interface IRefreshTokensRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);

    Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);

    Task RevokeAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);

    Task ReplaceAsync(RefreshToken oldRefreshToken, RefreshToken newRefreshToken, CancellationToken cancellationToken = default);
}