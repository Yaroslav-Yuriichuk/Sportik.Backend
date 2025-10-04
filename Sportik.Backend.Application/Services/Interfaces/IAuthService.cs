using Sportik.Backend.Application.DTOs.Auth;

namespace Sportik.Backend.Application.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResultDto?> LoginAsync(string email, string password);

    Task<AuthResultDto?> RefreshAsync(string refreshToken);
}