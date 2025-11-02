namespace Sportik.Backend.Domain.Models.Statistics;

public sealed class ExerciseStatistics
{
    public Exercise Exercise { get; init; } = new();

    public List<Set> Sets { get; init; } = new();
}