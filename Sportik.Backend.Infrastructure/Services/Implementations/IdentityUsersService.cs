using Microsoft.AspNetCore.Identity;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Entities;
using Sportik.Backend.Infrastructure.Identity;

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
        return appUser?.ToDomain();
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        ApplicationUser? appUser = await _userManager.FindByEmailAsync(email);
        return appUser?.ToDomain();
    }

    public async Task<User> CreateAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        ApplicationUser appUser = new()
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            CreatedAt = DateTimeOffset.UtcNow,
        };

        IdentityResult result = await _userManager.CreateAsync(appUser, password);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Failed to create user.");
        }

        return appUser.ToDomain();
    }
}