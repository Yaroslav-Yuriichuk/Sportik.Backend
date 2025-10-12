namespace Sportik.Backend.Domain.Entities.Statistics;

public sealed class DayStatistics
{
    public DateTime Date { get; init; }

    public List<ExerciseStatistics> ExerciseStatistics { get; init; } = new();
}