using Sportik.Backend.Domain.Entities;
using Sportik.Backend.Infrastructure.Identity;

namespace Sportik.Backend.Infrastructure.Persistence.Mappers;

internal static class UserMapper
{
    public static User ToDomain(ApplicationUser user)
    {
        return new User
        {
            Id = user.Id,
            Email = user.Email
        };
    }
}