using Microsoft.AspNetCore.Identity;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Models;
using Sportik.Backend.Infrastructure.Persistence.Entities.Identity;
using Sportik.Backend.Infrastructure.Persistence.Mappers;

namespace Sportik.Backend.Infrastructure.Services.Implementations;

internal sealed class IdentityUsersService : IUsersService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityUsersService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        ApplicationUser? appUser = await _userManager.FindByIdAsync(id.ToString());
        cancellationToken.ThrowIfCancellationRequested();

        return appUser is not null ? UserMapper.ToDomain(appUser) : null;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        ApplicationUser? appUser = await _userManager.FindByEmailAsync(email);
        cancellationToken.ThrowIfCancellationRequested();

        return appUser is not null ? UserMapper.ToDomain(appUser) : null;
    }

    public async Task<OperationResult<User>> CreateAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        ApplicationUser appUser = new()
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            CreatedAt = DateTimeOffset.UtcNow,
        };

        IdentityResult result = await _userManager.CreateAsync(appUser, password);
        cancellationToken.ThrowIfCancellationRequested();

        if (!result.Succeeded)
        {
            return OperationResult<User>.Failure(result.Errors.Select(e => e.Description));
        }

        return OperationResult<User>.Success(UserMapper.ToDomain(appUser));
    }
}