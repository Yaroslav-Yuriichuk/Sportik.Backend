using Sportik.Backend.Api.DTOs.Statistics;
using Sportik.Backend.Domain.Entities.Statistics;

namespace Sportik.Backend.Api.Mappers.Statistics;

internal static class WeekStatisticsMapper
{
    public static WeekStatisticsDto ToDto(WeekStatistics domain)
    {
        return new WeekStatisticsDto(
            DayStatistics: domain.DayStatistics
                .Select(DayStatisticsMapper.ToDto)
                .ToList());
    }
}