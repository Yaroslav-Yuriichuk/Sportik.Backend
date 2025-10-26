using Microsoft.EntityFrameworkCore;
using Sportik.Backend.Application.Repositories.Interfaces;
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

    public async Task<ExerciseSettings?> UpdateAsync(Guid userId, ExerciseSettingsDelta delta, Guid exerciseId,
        CancellationToken cancellationToken = default)
    {
        UserExercise? exerciseEntity = await _dbContext.Exercises
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.UserId == userId && e.Id == exerciseId, cancellationToken);

        if (exerciseEntity is null)
        {
            return null;
        }

        UserExerciseSettings? settingsEntity = await _dbContext.ExerciseSettings
            .FirstOrDefaultAsync(es => es.Id == exerciseEntity.SettingsId, cancellationToken);

        if (settingsEntity is null)
        {
            return null;
        }

        settingsEntity.TargetRepetitions = delta.TargetRepetitions ?? settingsEntity.TargetRepetitions;
        settingsEntity.TimeBetweenSets = delta.TimeBetweenSets ?? settingsEntity.TimeBetweenSets;
        settingsEntity.ExecutionTime = delta.ExecutionTime ?? settingsEntity.ExecutionTime;

        _dbContext.ExerciseSettings.Update(settingsEntity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ExerciseSettingsMapper.ToDomain(settingsEntity);
    }
}