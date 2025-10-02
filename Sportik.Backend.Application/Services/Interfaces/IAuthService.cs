using Sportik.Backend.Application.DTOs.Auth;

namespace Sportik.Backend.Application.Services.Interfaces;

public interface IAuthService
{
    Task<RegisterResultDto?> RegisterAsync(string email, string password);

    Task<LoginResultDto?> LoginAsync(string email, string password);
}