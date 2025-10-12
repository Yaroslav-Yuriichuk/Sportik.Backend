namespace Sportik.Backend.Api.DTOs.Statistics;

internal sealed record WeekStatisticsDto(List<DayStatisticsDto> DayStatistics);