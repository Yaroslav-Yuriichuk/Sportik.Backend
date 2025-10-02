using Microsoft.AspNetCore.Mvc;
using Sportik.Backend.Application.DTOs.Auth;
using Sportik.Backend.Application.Services.Interfaces;

namespace Sportik.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
    {
        RegisterResultDto? result = await _authService.RegisterAsync(registerRequest.Email, registerRequest.Password);

        if (result == null)
        {
            return BadRequest("User with this email already exists.");
        }

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        LoginResultDto? result = await _authService.LoginAsync(loginRequest.Email, loginRequest.Password);

        if (result == null)
        {
            return Unauthorized("Invalid email or password.");
        }

        return Ok(result);
    }
}