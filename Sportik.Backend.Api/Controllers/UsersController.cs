using Microsoft.AspNetCore.Mvc;
using Sportik.Backend.Api.DTOs.Users;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Entities;

namespace Sportik.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto, CancellationToken cancellationToken)
    {
        OperationResult<User> result = await _usersService.CreateAsync(registerRequestDto.Email, registerRequestDto.Password, cancellationToken);

        if (!result.Succeeded)
        {
            return BadRequest(new { result.Errors });
        }

        User user = result.Value!;
        return Ok(new RegisterResultDto(user.Id, user.Email!));
    }
}