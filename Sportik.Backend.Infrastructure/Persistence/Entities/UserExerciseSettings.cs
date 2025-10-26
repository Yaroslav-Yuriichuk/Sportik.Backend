namespace Sportik.Backend.Infrastructure.Persistence.Entities;

internal sealed class UserExerciseSettings
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public int TargetRepetitions { get; set; }

    public TimeSpan TimeBetweenSets { get; set; }

    public TimeSpan ExecutionTime { get; set; }
}