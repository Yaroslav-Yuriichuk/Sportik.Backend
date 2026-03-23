namespace Sportik.Backend.Api.DTOs.Exercises;

public sealed record AddExerciseDto(Guid? Id, string Name, AddExerciseSettingsDto Settings);