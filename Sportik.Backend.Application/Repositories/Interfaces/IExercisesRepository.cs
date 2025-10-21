using Sportik.Backend.Domain.Models;

namespace Sportik.Backend.Application.Repositories.Interfaces;

public interface IExercisesRepository
{
    Task<IEnumerable<Exercise>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<Exercise?> GetByIdAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);

    Task<Exercise> AddAsync(Guid userId, Exercise exercise, CancellationToken cancellationToken = default);

    Task<Exercise?> DeleteAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
}