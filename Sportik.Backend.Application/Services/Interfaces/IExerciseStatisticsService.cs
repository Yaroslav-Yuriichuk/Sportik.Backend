using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Models.Statistics;

namespace Sportik.Backend.Application.Services.Interfaces;

public interface IExerciseStatisticsService
{
    Task<IEnumerable<WeekStatistics>> GetAllAsync(Guid userId, WeekStatisticsOrder order, CancellationToken cancellationToken = default);

    Task<Set?> AddSetAsync(Guid userId, Set set, CancellationToken cancellationToken = default);
}