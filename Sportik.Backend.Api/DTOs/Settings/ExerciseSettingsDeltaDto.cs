namespace Sportik.Backend.Api.DTOs.Settings;

public sealed record ExerciseSettingsDeltaDto(int? TargetRepetitions, TimeSpan? TimeBetweenSets, TimeSpan? ExecutionTime);