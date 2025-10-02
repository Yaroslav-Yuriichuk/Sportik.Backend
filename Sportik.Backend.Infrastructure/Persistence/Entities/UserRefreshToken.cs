using Sportik.Backend.Infrastructure.Identity;

namespace Sportik.Backend.Infrastructure.Persistence.Entities;

internal sealed class UserRefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; init; } = null!;

    public string Hash { get; set; } = string.Empty;

    public DateTimeOffset ExpiresAt { get; init; }
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? RevokedAt { get; init; }

    public bool IsActive => RevokedAt == null && DateTimeOffset.UtcNow < ExpiresAt;
}