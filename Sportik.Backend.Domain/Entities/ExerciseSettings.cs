namespace Sportik.Backend.Domain.Entities;

public sealed class ExerciseSettings
{
    public int TargetRepetitions { get; init; }

    public TimeSpan TimeBetweenSets { get; init; }

    public TimeSpan ExecutionTime { get; init; }
}