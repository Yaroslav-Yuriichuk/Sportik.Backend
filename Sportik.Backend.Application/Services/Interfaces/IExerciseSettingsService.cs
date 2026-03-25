using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Models;
using Sportik.Backend.Domain.Models.Settings;

namespace Sportik.Backend.Application.Services.Interfaces;

public interface IExerciseSettingsService
{
    Task<OperationResult<Exercise>> UpdateAsync(Guid userId, UpdateExerciseSettingModel updateModel,
        CancellationToken cancellationToken = default);

        Task<OperationResult<IEnumerable<Exercise>>> UpdateRangeAsync(Guid userId, IEnumerable<UpdateExerciseSettingModel> updateModels,
            CancellationToken cancellationToken = default);
}