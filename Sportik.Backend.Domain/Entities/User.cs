namespace Sportik.Backend.Domain.Entities;

public sealed class User
{
    public Guid Id { get; init; }

    public string? Email { get; init; }
}