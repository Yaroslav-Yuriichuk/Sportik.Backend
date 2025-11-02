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