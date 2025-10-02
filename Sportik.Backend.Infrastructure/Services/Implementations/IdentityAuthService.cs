using Microsoft.AspNetCore.Identity;
using Sportik.Backend.Application.DTOs.Auth;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Entities;
using Sportik.Backend.Infrastructure.Identity;

namespace Sportik.Backend.Infrastructure.Services.Implementations;

internal sealed class IdentityAuthService : IAuthService
{
    private readonly IUsersService _usersService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityAuthService(IUsersService usersService, SignInManager<ApplicationUser> signInManager)
    {
        _usersService = usersService;
        _signInManager = signInManager;
    }

    public async Task<RegisterResultDto?> RegisterAsync(string email, string password)
    {
        User? existingUser = await _usersService.GetByEmailAsync(email);

        if (existingUser != null)
        {
            return null;
        }

        User newUser = await _usersService.CreateAsync(email, password);
        return new RegisterResultDto(newUser.Id);
    }

    public async Task<LoginResultDto?> LoginAsync(string email, string password)
    {
        SignInResult signInResult = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);

        if (!signInResult.Succeeded)
        {
            return null;
        }

        return new LoginResultDto(
            AccessToken: "dummy_access_token",
            RefreshToken: "dummy_refresh_token",
            TokenType: "Bearer",
            ExpiresIn: 3600
        );
    }
}