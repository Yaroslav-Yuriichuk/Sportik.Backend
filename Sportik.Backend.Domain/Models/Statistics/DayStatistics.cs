namespace Sportik.Backend.Domain.Models.Statistics;

public sealed class DayStatistics
{
    public DateTime Date { get; init; }

    public List<ExerciseStatistics> ExerciseStatistics { get; init; } = new();
}