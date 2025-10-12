using Sportik.Backend.Api.DTOs.Statistics;
using Sportik.Backend.Domain.Entities.Statistics;

namespace Sportik.Backend.Api.Mappers.Statistics;

internal static class DayStatisticsMapper
{
    public static DayStatisticsDto ToDto(DayStatistics domain)
    {
        return new DayStatisticsDto(
            Date: domain.Date,
            ExerciseStatistics: domain.ExerciseStatistics
                .Select(ExerciseStatisticsMapper.ToDto)
                .ToList());
    }
}