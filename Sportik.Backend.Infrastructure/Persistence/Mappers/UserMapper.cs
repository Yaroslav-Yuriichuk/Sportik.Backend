using Sportik.Backend.Domain.Models;
using Sportik.Backend.Infrastructure.Persistence.Entities.Identity;

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