namespace Sportik.Backend.Api.DTOs.Statistics;

public sealed record AddSetDto(Guid ExerciseId, int Repetitions, DateTimeOffset CreatedAt);