using Sportik.Backend.Domain.Entities.Statistics;

namespace Sportik.Backend.Application.Repositories.Interfaces;

public interface IExerciseSetsRepository
{
    Task<IEnumerable<Set>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<Set?> AddAsync(Guid userId, Set set, CancellationToken cancellationToken = default);
}