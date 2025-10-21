using Microsoft.EntityFrameworkCore;
using Sportik.Backend.Application.Repositories.Interfaces;
using Sportik.Backend.Domain.Models;
using Sportik.Backend.Infrastructure.Persistence;
using Sportik.Backend.Infrastructure.Persistence.Entities;
using Sportik.Backend.Infrastructure.Persistence.Mappers;

namespace Sportik.Backend.Infrastructure.Repositories.Implementations;

internal sealed class ExercisesRepository : IExercisesRepository
{
    private readonly AppDbContext _dbContext;

    public ExercisesRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Exercise>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        List<UserExercise> entities = await _dbContext.Exercises
            .AsNoTracking()
            .Include(e => e.Settings)
            .Where(e => e.UserId == userId)
            .ToListAsync(cancellationToken);

        return entities.Select(ExerciseMapper.ToDomain);
    }

    public async Task<Exercise?> GetByIdAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        UserExercise? entity = await _dbContext.Exercises
            .AsNoTracking()
            .Include(e => e.Settings)
            .FirstOrDefaultAsync(e => e.UserId == userId && e.Id == exerciseId, cancellationToken);

        return entity is null ? null : ExerciseMapper.ToDomain(entity);
    }

    public async Task<Exercise> AddAsync(Guid userId, Exercise exercise, CancellationToken cancellationToken = default)
    {
        UserExercise entity = ExerciseMapper.ToEntity(exercise, userId);

        await _dbContext.Exercises.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ExerciseMapper.ToDomain(entity);
    }

    public async Task<Exercise?> DeleteAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        UserExercise? entity = await _dbContext.Exercises
            .FirstOrDefaultAsync(e => e.UserId == userId && e.Id == exerciseId, cancellationToken);

        if (entity is null)
        {
            return null;
        }

        _dbContext.Exercises.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ExerciseMapper.ToDomain(entity);

    }
}