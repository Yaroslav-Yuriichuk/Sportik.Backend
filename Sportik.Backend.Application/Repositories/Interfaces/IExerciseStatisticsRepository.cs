using Sportik.Backend.Domain.Models.Statistics;

namespace Sportik.Backend.Application.Repositories.Interfaces;

public interface IExerciseStatisticsRepository
{
    Task<IEnumerable<IGrouping<TKey, ExerciseStatistics>>> GetAllAsync<TKey>(Guid userId, Func<Set, TKey> key,
        CancellationToken cancellationToken = default);
}