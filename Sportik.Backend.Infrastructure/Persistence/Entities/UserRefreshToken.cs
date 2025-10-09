using Sportik.Backend.Infrastructure.Persistence.Entities.Identity;

namespace Sportik.Backend.Infrastructure.Persistence.Entities;

internal sealed class UserRefreshToken
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid UserId { get; init; }

    public string Hash { get; init; } = string.Empty;

    public DateTimeOffset ExpiresAt { get; init; }

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? RevokedAt { get; set; }

    public ApplicationUser User { get; init; } = null!;

    public bool IsActive => RevokedAt == null && DateTimeOffset.UtcNow < ExpiresAt;
}