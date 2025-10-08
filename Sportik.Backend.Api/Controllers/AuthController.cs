using Microsoft.AspNetCore.Mvc;
using Sportik.Backend.Api.DTOs.Auth;
using Sportik.Backend.Api.Mappers;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Entities;

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
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDro)
    {
        OperationResult<AuthTokens> result = await _authService.LoginAsync(loginRequestDro.Email, loginRequestDro.Password);

        if (!result.Succeeded)
        {
            return Unauthorized(new { result.Errors });
        }

        return Ok(AuthTokensMapper.ToDto(result.Value!));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto refreshTokenRequestDto)
    {
        OperationResult<AuthTokens> result = await _authService.RefreshAsync(refreshTokenRequestDto.RefreshToken);

        if (!result.Succeeded)
        {
            return Unauthorized(new { result.Errors });
        }

        return Ok(AuthTokensMapper.ToDto(result.Value!));
    }
}