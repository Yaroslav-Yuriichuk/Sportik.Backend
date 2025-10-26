namespace Sportik.Backend.Domain.Models.Settings;

public sealed class ExerciseSettingsDelta
{
    public int? TargetRepetitions { get; init; }

    public TimeSpan? TimeBetweenSets { get; init; }

    public TimeSpan? ExecutionTime { get; init; }
}