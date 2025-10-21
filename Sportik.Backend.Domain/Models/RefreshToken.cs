namespace Sportik.Backend.Domain.Models;

public sealed class RefreshToken
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public string Token { get; init; } = string.Empty;

    public DateTimeOffset ExpiresAt { get; init; }

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset? RevokedAt { get; init; }

    public bool IsActive => RevokedAt == null && ExpiresAt > DateTimeOffset.UtcNow;
}