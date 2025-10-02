using Microsoft.AspNetCore.Identity;
using Sportik.Backend.Domain.Entities;

namespace Sportik.Backend.Infrastructure.Identity;

internal sealed class ApplicationUser : IdentityUser<Guid>
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public User ToDomain()
    {
        return new User
        {
            Id = Id,
            Email = Email
        };
    }
}