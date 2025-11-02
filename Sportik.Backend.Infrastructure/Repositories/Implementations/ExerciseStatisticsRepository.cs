using Microsoft.EntityFrameworkCore;
using Sportik.Backend.Application.Repositories.Interfaces;
using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Models.Statistics;
using Sportik.Backend.Infrastructure.Persistence;
using Sportik.Backend.Infrastructure.Persistence.Entities;
using Sportik.Backend.Infrastructure.Persistence.Mappers;

namespace Sportik.Backend.Infrastructure.Repositories.Implementations;

internal sealed class ExerciseStatisticsRepository : IExerciseStatisticsRepository
{
    private readonly AppDbContext _dbContext;

    public ExerciseStatisticsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<IGrouping<TKey, ExerciseStatistics>>> GetAllAsync<TKey>(Guid userId, Func<Set, TKey> key,
        CancellationToken cancellationToken = default)
    {
        IEnumerable<UserSet> sets = await _dbContext.Sets
            .AsNoTracking()
            .Include(s => s.Exercise)
            .ThenInclude(e => e.Settings)
            .Where(s => s.UserId == userId)
            .ToListAsync(cancellationToken);

        return sets
            .OrderBy(s => s.LoggedAt)
            .GroupBy(s => key(SetMapper.ToDomain(s)))
            .Select(group =>
            {
                TKey groupKey = group.Key;

                IEnumerable<ExerciseStatistics> exerciseStatistics = group
                    .GroupBy(s => s.ExerciseId)
                    .Select(g => new ExerciseStatistics
                    {
                        Exercise = ExerciseMapper.ToDomain(g.First().Exercise),
                        Sets = g
                            .Select(SetMapper.ToDomain)
                            .ToList(),
                    });

                return (IGrouping<TKey, ExerciseStatistics>)new Grouping<TKey, ExerciseStatistics>(groupKey, exerciseStatistics);
            });
    }
}