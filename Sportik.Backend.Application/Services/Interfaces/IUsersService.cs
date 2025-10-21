using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Models;

namespace Sportik.Backend.Application.Services.Interfaces;

public interface IUsersService
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<OperationResult<User>> CreateAsync(string email, string password, CancellationToken cancellationToken = default);
}