using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Entities;

namespace Sportik.Backend.Application.Services.Interfaces;

public interface IAuthService
{
    Task<OperationResult<AuthTokens>> LoginAsync(string email, string password, CancellationToken cancellationToken = default);

    Task<OperationResult<AuthTokens>> RefreshAsync(string refreshToken, CancellationToken cancellationToken = default);
}