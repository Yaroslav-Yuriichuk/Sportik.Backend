using Microsoft.EntityFrameworkCore;
using Sportik.Backend.Application.Repositories.Interfaces;
using Sportik.Backend.Domain.Models.Statistics;
using Sportik.Backend.Infrastructure.Persistence;
using Sportik.Backend.Infrastructure.Persistence.Entities;
using Sportik.Backend.Infrastructure.Persistence.Mappers;

namespace Sportik.Backend.Infrastructure.Repositories.Implementations;

internal sealed class ExerciseSetsRepository : IExerciseSetsRepository
{
    private readonly AppDbContext _dbContext;

    public ExerciseSetsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Set>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        List<UserSet> setEntities = await _dbContext.Sets
            .AsNoTracking()
            .Where(s => s.UserId == userId)
            .ToListAsync(cancellationToken);

        return setEntities.Select(SetMapper.ToDomain);
    }

    public async Task<Set?> AddAsync(Guid userId, Set set, CancellationToken cancellationToken = default)
    {
        bool hasExercise = await _dbContext.Exercises
            .AsNoTracking()
            .AnyAsync(e => e.UserId == userId && e.Id == set.ExerciseId, cancellationToken);

        if (!hasExercise)
        {
            return null;
        }

        UserSet setEntity = SetMapper.ToEntity(set, userId);

        await _dbContext.Sets.AddAsync(setEntity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return SetMapper.ToDomain(setEntity);
    }

    public async Task<IEnumerable<Set>> AddRangeAsync(Guid userId, IEnumerable<Set> sets, CancellationToken cancellationToken = default)
    {
        sets = sets as IList<Set> ?? sets.ToList();
        List<Guid> exerciseIds = sets.Select(s => s.ExerciseId).Distinct().ToList();

        HashSet<Guid> existingExerciseIds = (await _dbContext.Exercises
                .AsNoTracking()
                .Where(e => e.UserId == userId && exerciseIds.Contains(e.Id))
                .Select(e => e.Id)
                .ToListAsync(cancellationToken))
            .ToHashSet();

        List<UserSet> setEntities = new List<UserSet>();

        foreach (Set set in sets)
        {
            if (!existingExerciseIds.Contains(set.ExerciseId))
            {
                continue;
            }

            UserSet setEntity = SetMapper.ToEntity(set, userId);
            setEntities.Add(setEntity);
        }

        await _dbContext.Sets.AddRangeAsync(setEntities, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return setEntities.Select(SetMapper.ToDomain);
    }
}