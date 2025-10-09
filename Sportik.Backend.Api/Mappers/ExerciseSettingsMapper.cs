using Sportik.Backend.Api.DTOs.Exercises;
using Sportik.Backend.Domain.Entities;

namespace Sportik.Backend.Api.Mappers;

internal static class ExerciseSettingsMapper
{
    public static ExerciseSettingsDto ToDto(ExerciseSettings domain)
    {
        return new ExerciseSettingsDto(
            TargetRepetitions: domain.TargetRepetitions,
            TimeBetweenSets: domain.TimeBetweenSets,
            ExecutionTime: domain.ExecutionTime);
    }

    public static ExerciseSettings ToDomain(ExerciseSettingsDto dto)
    {
        return new ExerciseSettings
        {
            TargetRepetitions = dto.TargetRepetitions,
            TimeBetweenSets = dto.TimeBetweenSets,
            ExecutionTime = dto.ExecutionTime
        };
    }

    public static ExerciseSettings ToDomain(AddExerciseSettingsDto dto)
    {
        return new ExerciseSettings
        {
            TargetRepetitions = dto.TargetRepetitions,
            TimeBetweenSets = dto.TimeBetweenSets,
            ExecutionTime = dto.ExecutionTime
        };
    }
}