namespace Sportik.Backend.Domain.Entities.Statistics;

public sealed class ExerciseStatistics
{
    public Guid ExerciseId { get; init; }

    public List<Set> Sets { get; init; } = new();
}