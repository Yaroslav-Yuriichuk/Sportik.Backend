using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Models.Statistics;

namespace Sportik.Backend.Application.Services.Interfaces;

public interface IExerciseStatisticsService
{
    Task<IEnumerable<Set>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<IEnumerable<WeekStatistics>> GetAllAsync(Guid userId, WeekStatisticsOrder order, TimeSpan offset,
        CancellationToken cancellationToken = default);

    Task<Set?> AddAsync(Guid userId, Set set, CancellationToken cancellationToken = default);

    Task<IEnumerable<Set>> AddRangeAsync(Guid userId, IEnumerable<Set> sets, CancellationToken cancellationToken = default);
}