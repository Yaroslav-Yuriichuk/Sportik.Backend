namespace Sportik.Backend.Domain.Models;

public sealed class Exercise
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public ExerciseSettings Settings { get; init; } = null!;
}