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
        UserExercise? exerciseEntity = await _dbContext.Exercises
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.UserId == userId && e.Id == set.ExerciseId, cancellationToken);

        if (exerciseEntity is null)
        {
            return null;
        }

        UserSet setEntity = SetMapper.ToEntity(set, userId);

        await _dbContext.Sets.AddAsync(setEntity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return SetMapper.ToDomain(setEntity);
    }
}