using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Models.Settings;

namespace Sportik.Backend.Application.Services.Interfaces;

public interface IExerciseSettingsService
{
    Task<OperationResult<ExerciseSettings>> UpdateAsync(Guid userId, ExerciseSettingsDelta delta, Guid exerciseId,
        CancellationToken cancellationToken = default);
}