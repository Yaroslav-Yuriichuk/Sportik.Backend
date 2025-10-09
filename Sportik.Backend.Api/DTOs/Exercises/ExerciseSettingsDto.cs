namespace Sportik.Backend.Api.DTOs.Exercises;

internal sealed record ExerciseSettingsDto(int TargetRepetitions, TimeSpan TimeBetweenSets, TimeSpan ExecutionTime);