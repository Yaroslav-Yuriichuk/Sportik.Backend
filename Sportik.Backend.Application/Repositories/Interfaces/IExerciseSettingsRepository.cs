using Sportik.Backend.Domain.Models;
using Sportik.Backend.Domain.Models.Settings;

namespace Sportik.Backend.Application.Repositories.Interfaces;

public interface IExerciseSettingsRepository
{
    Task<Exercise?> UpdateAsync(Guid userId, UpdateExerciseSettingModel updateModel, CancellationToken cancellationToken = default);

    Task<IEnumerable<Exercise>> UpdateRangeAsync(Guid userId, IEnumerable<UpdateExerciseSettingModel> updateModels,
        CancellationToken cancellationToken = default);
}