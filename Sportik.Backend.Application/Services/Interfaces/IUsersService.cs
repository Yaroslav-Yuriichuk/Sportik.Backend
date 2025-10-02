using Sportik.Backend.Domain.Entities;

namespace Sportik.Backend.Application.Services.Interfaces;

public interface IUsersService
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<User> CreateAsync(string email, string password, CancellationToken cancellationToken = default);
}