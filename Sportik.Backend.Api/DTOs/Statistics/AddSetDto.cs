namespace Sportik.Backend.Api.DTOs.Statistics;

public sealed record AddSetDto(Guid? Id, Guid ExerciseId, int Repetitions, DateTimeOffset LoggedAt);