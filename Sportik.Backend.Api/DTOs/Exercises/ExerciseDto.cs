namespace Sportik.Backend.Api.DTOs.Exercises;

internal sealed record ExerciseDto(Guid Id, string Name, ExerciseSettingsDto Settings);