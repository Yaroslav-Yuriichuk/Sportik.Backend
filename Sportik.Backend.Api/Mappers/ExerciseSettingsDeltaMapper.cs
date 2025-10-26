using Sportik.Backend.Api.DTOs.Settings;
using Sportik.Backend.Domain.Models.Settings;

namespace Sportik.Backend.Api.Mappers;

internal static class ExerciseSettingsDeltaMapper
{
    public static ExerciseSettingsDelta ToDomain(ExerciseSettingsDeltaDto dto)
    {
        return new ExerciseSettingsDelta
        {
            TargetRepetitions = dto.TargetRepetitions,
            TimeBetweenSets = dto.TimeBetweenSets,
            ExecutionTime = dto.ExecutionTime
        };
    }
}