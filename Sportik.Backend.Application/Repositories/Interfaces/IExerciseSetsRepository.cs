using Sportik.Backend.Domain.Models.Statistics;

namespace Sportik.Backend.Application.Repositories.Interfaces;

public interface IExerciseSetsRepository
{
    Task<Set?> AddAsync(Guid userId, Set set, CancellationToken cancellationToken = default);
}