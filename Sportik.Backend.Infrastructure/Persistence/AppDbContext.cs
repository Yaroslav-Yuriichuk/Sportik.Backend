using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sportik.Backend.Infrastructure.Identity;
using Sportik.Backend.Infrastructure.Persistence.Entities;

namespace Sportik.Backend.Infrastructure.Persistence;

internal sealed class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<UserRefreshToken> RefreshTokens { get; set; } = null!;
}