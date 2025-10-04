using Microsoft.AspNetCore.Identity;
using Sportik.Backend.Application.DTOs.Auth;
using Sportik.Backend.Application.Repositories.Interfaces;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Entities;
using Sportik.Backend.Infrastructure.Identity;

namespace Sportik.Backend.Infrastructure.Services.Implementations;

internal sealed class IdentityAuthService : IAuthService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUsersService _usersService;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokensRepository _refreshTokensRepository;

    public IdentityAuthService(SignInManager<ApplicationUser> signInManager, IUsersService usersService,
        ITokenService tokenService, IRefreshTokensRepository refreshTokensRepository)
    {
        _signInManager = signInManager;
        _usersService = usersService;
        _tokenService = tokenService;
        _refreshTokensRepository = refreshTokensRepository;
    }

    public async Task<AuthResultDto?> LoginAsync(string email, string password)
    {
        SignInResult signInResult = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);

        if (!signInResult.Succeeded)
        {
            return null;
        }

        User? user = await _usersService.GetByEmailAsync(email);

        if (user == null)
        {
            return null;
        }

        AccessToken accessToken = _tokenService.GenerateAccessToken(user.Id, user.Email!);
        RefreshToken refreshToken = _tokenService.GenerateRefreshToken(user.Id);

        await _refreshTokensRepository.AddAsync(refreshToken);

        return new AuthResultDto(
            AccessToken: accessToken.Token,
            RefreshToken: refreshToken.Token,
            TokenType: "Bearer",
            ExpiresIn: (int)(accessToken.ExpiresAt - DateTimeOffset.UtcNow).TotalSeconds
        );
    }

    public async Task<AuthResultDto?> RefreshAsync(string refreshToken)
    {
        RefreshToken? existingRefreshToken = await _refreshTokensRepository.GetByTokenAsync(refreshToken);

        if (existingRefreshToken is not { IsActive: true })
        {
            return null;
        }

        User? user = await _usersService.GetByIdAsync(existingRefreshToken.UserId);

        if (user == null)
        {
            return null;
        }

        AccessToken newAccessToken = _tokenService.GenerateAccessToken(user.Id, user.Email!);
        RefreshToken newRefreshToken = _tokenService.GenerateRefreshToken(user.Id);

        await _refreshTokensRepository.ReplaceAsync(existingRefreshToken, newRefreshToken);

        return new AuthResultDto(
            AccessToken: newAccessToken.Token,
            RefreshToken: newRefreshToken.Token,
            TokenType: "Bearer",
            ExpiresIn: (int)(newAccessToken.ExpiresAt - DateTimeOffset.UtcNow).TotalSeconds
        );
    }
}