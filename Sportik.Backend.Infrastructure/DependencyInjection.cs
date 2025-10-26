using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sportik.Backend.Application.Repositories.Interfaces;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Infrastructure.Persistence;
using Sportik.Backend.Infrastructure.Persistence.Entities.Identity;
using Sportik.Backend.Infrastructure.Repositories.Implementations;
using Sportik.Backend.Infrastructure.Services.Implementations;

namespace Sportik.Backend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();

        services.AddScoped<IUsersService, IdentityUsersService>();
        services.AddScoped<IAuthService, IdentityAuthService>();

        services.AddScoped<IExercisesRepository, ExercisesRepository>();
        services.AddScoped<IExerciseSettingsRepository, ExerciseSettingsRepository>();
        services.AddScoped<IExerciseSetsRepository, ExerciseSetsRepository>();

        return services;
    }
}