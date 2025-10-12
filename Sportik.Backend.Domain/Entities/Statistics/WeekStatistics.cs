namespace Sportik.Backend.Domain.Entities.Statistics;

public sealed class WeekStatistics
{
    public List<DayStatistics> DayStatistics { get; init; } = new();
}