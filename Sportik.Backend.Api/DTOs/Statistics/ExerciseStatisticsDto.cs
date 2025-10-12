namespace Sportik.Backend.Api.DTOs.Statistics;

internal sealed record ExerciseStatisticsDto(Guid ExerciseId, List<SetDto> Sets);