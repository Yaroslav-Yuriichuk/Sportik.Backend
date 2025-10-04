using Microsoft.AspNetCore.Mvc;
using Sportik.Backend.Application.DTOs.Auth;
using Sportik.Backend.Application.Services.Interfaces;

namespace Sportik.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        AuthResultDto? result = await _authService.LoginAsync(loginRequest.Email, loginRequest.Password);

        if (result == null)
        {
            return Unauthorized("Invalid email or password.");
        }

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto refreshTokenRequest)
    {
        AuthResultDto? result = await _authService.RefreshAsync(refreshTokenRequest.RefreshToken);

        if (result == null)
        {
            return Unauthorized("Invalid refresh token.");
        }

        return Ok(result);
    }
}