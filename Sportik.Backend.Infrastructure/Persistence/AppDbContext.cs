using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sportik.Backend.Infrastructure.Persistence.Entities;
using Sportik.Backend.Infrastructure.Persistence.Entities.Identity;

namespace Sportik.Backend.Infrastructure.Persistence;

internal sealed class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<UserRefreshToken> RefreshTokens { get; set; } = null!;

    public DbSet<UserExercise> Exercises { get; set; } = null!;

    public DbSet<UserExerciseSettings> ExerciseSettings { get; set; } = null!;

    public DbSet<UserSet> Sets { get; set; } = null!;
}