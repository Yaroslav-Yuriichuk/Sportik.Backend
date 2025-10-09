using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Sportik.Backend.Application.Repositories.Interfaces;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Entities;
using Sportik.Backend.Infrastructure.Persistence.Entities.Identity;

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

    public async Task<OperationResult<AuthTokens>> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        SignInResult signInResult = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);
        cancellationToken.ThrowIfCancellationRequested();

        if (!signInResult.Succeeded)
        {
            return OperationResult<AuthTokens>.Failure(new[] { "Invalid email or password." });
        }

        User? user = await _usersService.GetByEmailAsync(email, cancellationToken);

        if (user == null)
        {
            return OperationResult<AuthTokens>.Failure(new[] { "User not found." });
        }

        AccessToken accessToken = _tokenService.GenerateAccessToken(user.Id, user.Email!);
        RefreshToken refreshToken = _tokenService.GenerateRefreshToken(user.Id);

        await _refreshTokensRepository.AddAsync(refreshToken, cancellationToken);

        return OperationResult<AuthTokens>.Success(new AuthTokens
        {
            AccessToken = accessToken.Token,
            RefreshToken = refreshToken.Token,
            TokenType = JwtBearerDefaults.AuthenticationScheme,
            ExpiresIn = (int)(accessToken.ExpiresAt - DateTimeOffset.UtcNow).TotalSeconds,
        });
    }

    public async Task<OperationResult<AuthTokens>> RefreshAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        RefreshToken? existingRefreshToken = await _refreshTokensRepository.GetByTokenAsync(refreshToken, cancellationToken);

        if (existingRefreshToken is not { IsActive: true })
        {
            return OperationResult<AuthTokens>.Failure(new[] { "Invalid refresh token." });
        }

        User? user = await _usersService.GetByIdAsync(existingRefreshToken.UserId, cancellationToken);

        if (user == null)
        {
            return OperationResult<AuthTokens>.Failure(new[] { "User not found." });
        }

        AccessToken newAccessToken = _tokenService.GenerateAccessToken(user.Id, user.Email!);
        RefreshToken newRefreshToken = _tokenService.GenerateRefreshToken(user.Id);

        await _refreshTokensRepository.ReplaceAsync(refreshToken, newRefreshToken, cancellationToken);

        return OperationResult<AuthTokens>.Success(new AuthTokens
        {
            AccessToken = newAccessToken.Token,
            RefreshToken = newRefreshToken.Token,
            TokenType = JwtBearerDefaults.AuthenticationScheme,
            ExpiresIn = (int)(newAccessToken.ExpiresAt - DateTimeOffset.UtcNow).TotalSeconds,
        });
    }
}