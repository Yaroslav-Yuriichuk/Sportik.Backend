using Sportik.Backend.Domain.Models.Statistics;

namespace Sportik.Backend.Application.Repositories.Interfaces;

public interface IExerciseSetsRepository
{
    Task<IEnumerable<Set>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<Set?> AddAsync(Guid userId, Set set, CancellationToken cancellationToken = default);

    Task<IEnumerable<Set>> AddRangeAsync(Guid userId, IEnumerable<Set> sets, CancellationToken cancellationToken = default);
}