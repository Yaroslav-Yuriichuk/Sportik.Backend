using Microsoft.EntityFrameworkCore;
using Sportik.Backend.Application.Repositories.Interfaces;
using Sportik.Backend.Domain.Models;
using Sportik.Backend.Domain.Models.Settings;
using Sportik.Backend.Infrastructure.Persistence;
using Sportik.Backend.Infrastructure.Persistence.Entities;
using Sportik.Backend.Infrastructure.Persistence.Mappers;

namespace Sportik.Backend.Infrastructure.Repositories.Implementations;

internal sealed class ExerciseSettingsRepository : IExerciseSettingsRepository
{
    private readonly AppDbContext _dbContext;

    public ExerciseSettingsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Exercise?> UpdateAsync(Guid userId, UpdateExerciseSettingModel updateModel,
        CancellationToken cancellationToken = default)
    {
        UserExercise? exerciseEntity = await _dbContext.Exercises
            .Include(e => e.Settings)
            .FirstOrDefaultAsync(e => e.UserId == userId && e.Id == updateModel.ExerciseId, cancellationToken);

        if (exerciseEntity?.Settings is null)
        {
            return null;
        }

        UserExerciseSettings settingsEntity = exerciseEntity.Settings;
        ExerciseSettingsDelta delta = updateModel.Delta;

        settingsEntity.TargetRepetitions = delta.TargetRepetitions ?? settingsEntity.TargetRepetitions;
        settingsEntity.TimeBetweenSets = delta.TimeBetweenSets ?? settingsEntity.TimeBetweenSets;
        settingsEntity.ExecutionTime = delta.ExecutionTime ?? settingsEntity.ExecutionTime;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return ExerciseMapper.ToDomain(exerciseEntity);
    }

    public async Task<IEnumerable<Exercise>> UpdateRangeAsync(Guid userId,
        IEnumerable<UpdateExerciseSettingModel> updateModels,
        CancellationToken cancellationToken = default)
    {
        List<UpdateExerciseSettingModel> models = updateModels.ToList();

        if (models.Count == 0)
        {
            return Enumerable.Empty<Exercise>();
        }

        List<Guid> exerciseIds = models.Select(m => m.ExerciseId).ToList();

        List<UserExercise> exercisesEntities = await _dbContext.Exercises
            .Include(e => e.Settings)
            .Where(e => e.UserId == userId && exerciseIds.Contains(e.Id))
            .ToListAsync(cancellationToken);

        if (exercisesEntities.Count == 0)
        {
            return Enumerable.Empty<Exercise>();
        }

        Dictionary<Guid, UserExercise> exercisesById = exercisesEntities.ToDictionary(e => e.Id);
        List<Exercise> updatedExercises = new List<Exercise>();

        foreach (UpdateExerciseSettingModel model in models)
        {
            if (!exercisesById.TryGetValue(model.ExerciseId, out UserExercise? exerciseEntity))
            {
                continue;
            }

            UserExerciseSettings settingsEntity = exerciseEntity.Settings;
            ExerciseSettingsDelta delta = model.Delta;

            settingsEntity.TargetRepetitions = delta.TargetRepetitions ?? settingsEntity.TargetRepetitions;
            settingsEntity.TimeBetweenSets = delta.TimeBetweenSets ?? settingsEntity.TimeBetweenSets;
            settingsEntity.ExecutionTime = delta.ExecutionTime ?? settingsEntity.ExecutionTime;

            updatedExercises.Add(ExerciseMapper.ToDomain(exerciseEntity));
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return updatedExercises;
    }
}