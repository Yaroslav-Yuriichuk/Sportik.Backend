using Sportik.Backend.Domain.Models;

namespace Sportik.Backend.Application.Services.Interfaces;

public interface IExercisesService
{
    Task<IEnumerable<Exercise>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<Exercise?> GetByIdAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);

    Task<Exercise> AddAsync(Guid userId, Exercise exercise, CancellationToken cancellationToken = default);

    Task<IEnumerable<Exercise>> AddRangeAsync(Guid userId, IEnumerable<Exercise> exercises, CancellationToken cancellationToken = default);

    Task<Exercise?> DeleteAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
}