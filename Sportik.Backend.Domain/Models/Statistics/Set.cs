namespace Sportik.Backend.Domain.Models.Statistics;

public sealed class Set
{
    public Guid Id { get; init; }

    public int Repetitions { get; init; }

    public DateTimeOffset LoggedAt { get; init; }

    public Guid ExerciseId { get; init; }
}