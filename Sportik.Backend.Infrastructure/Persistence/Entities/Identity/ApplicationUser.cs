using Microsoft.AspNetCore.Identity;

namespace Sportik.Backend.Infrastructure.Persistence.Entities.Identity;

internal sealed class ApplicationUser : IdentityUser<Guid>
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}