using Sportik.Backend.Api.DTOs.Statistics;
using Sportik.Backend.Domain.Models.Statistics;

namespace Sportik.Backend.Api.Mappers.Statistics;

internal static class ExerciseStatisticsMapper
{
    public static ExerciseStatisticsDto ToDto(ExerciseStatistics domain)
    {
        return new ExerciseStatisticsDto(
            Exercise: ExerciseMapper.ToDto(domain.Exercise),
            Sets: domain.Sets.Select(SetMapper.ToDto).ToList());
    }
}