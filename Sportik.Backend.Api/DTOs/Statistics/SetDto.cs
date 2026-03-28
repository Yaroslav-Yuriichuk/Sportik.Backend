namespace Sportik.Backend.Api.DTOs.Statistics;

internal sealed record SetDto(Guid Id, int Repetitions, DateTimeOffset LoggedAt, Guid ExerciseId);