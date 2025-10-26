using Sportik.Backend.Domain.Models.Settings;

namespace Sportik.Backend.Application.Repositories.Interfaces;

public interface IExerciseSettingsRepository
{
    Task<ExerciseSettings?> UpdateAsync(Guid userId, ExerciseSettingsDelta delta, Guid exerciseId, CancellationToken cancellationToken = default);
}