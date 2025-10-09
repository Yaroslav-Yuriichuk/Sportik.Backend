namespace Sportik.Backend.Infrastructure.Persistence.Entities;

internal sealed class UserExerciseSettings
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public int TargetRepetitions { get; init; }

    public TimeSpan TimeBetweenSets { get; init; }

    public TimeSpan ExecutionTime { get; init; }
}