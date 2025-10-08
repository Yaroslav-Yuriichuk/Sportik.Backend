using Microsoft.Extensions.DependencyInjection;
using Sportik.Backend.Application.Services.Implementations;
using Sportik.Backend.Application.Services.Interfaces;

namespace Sportik.Backend.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IExercisesService, ExercisesService>();

        return services;
    }
}