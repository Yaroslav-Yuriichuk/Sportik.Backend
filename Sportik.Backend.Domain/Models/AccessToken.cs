namespace Sportik.Backend.Domain.Models;

public sealed class AccessToken
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid UserId { get; init; }

    public string Token { get; init; } = string.Empty;

    public DateTimeOffset ExpiresAt { get; init; }
}