namespace Sportik.Backend.Api.DTOs.Statistics;

internal sealed record DayStatisticsDto(DateTime Date, List<ExerciseStatisticsDto> ExerciseStatistics);