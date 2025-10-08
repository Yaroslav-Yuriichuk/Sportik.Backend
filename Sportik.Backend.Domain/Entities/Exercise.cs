namespace Sportik.Backend.Domain.Entities;

public sealed class Exercise
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
}