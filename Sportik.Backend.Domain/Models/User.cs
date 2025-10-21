namespace Sportik.Backend.Domain.Models;

public sealed class User
{
    public Guid Id { get; init; }

    public string? Email { get; init; }
}