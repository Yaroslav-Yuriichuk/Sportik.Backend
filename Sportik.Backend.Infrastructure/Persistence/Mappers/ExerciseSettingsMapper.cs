using Sportik.Backend.Domain.Models;
using Sportik.Backend.Infrastructure.Persistence.Entities;

namespace Sportik.Backend.Infrastructure.Persistence.Mappers;

internal static class ExerciseSettingsMapper
{
    public static ExerciseSettings ToDomain(UserExerciseSettings entity)
    {
        return new ExerciseSettings
        {
            TargetRepetitions = entity.TargetRepetitions,
            TimeBetweenSets = entity.TimeBetweenSets,
            ExecutionTime = entity.ExecutionTime,
        };
    }

    public static UserExerciseSettings ToEntity(ExerciseSettings domain)
    {
        return new UserExerciseSettings
        {
            TargetRepetitions = domain.TargetRepetitions,
            TimeBetweenSets = domain.TimeBetweenSets,
            ExecutionTime = domain.ExecutionTime,
        };
    }
}