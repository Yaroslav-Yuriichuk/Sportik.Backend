namespace Sportik.Backend.Domain.Models.Statistics;

public sealed class WeekStatistics
{
    public List<DayStatistics> DayStatistics { get; init; } = new();
}