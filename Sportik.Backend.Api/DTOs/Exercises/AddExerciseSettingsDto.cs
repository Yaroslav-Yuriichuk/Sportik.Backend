namespace Sportik.Backend.Api.DTOs.Exercises;

public sealed record AddExerciseSettingsDto(int TargetRepetitions, TimeSpan TimeBetweenSets, TimeSpan ExecutionTime);