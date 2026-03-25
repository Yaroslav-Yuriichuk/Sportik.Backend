using Sportik.Backend.Application.Repositories.Interfaces;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Models;
using Sportik.Backend.Domain.Models.Settings;

namespace Sportik.Backend.Application.Services.Implementations;

internal sealed class ExerciseSettingsService : IExerciseSettingsService
{
    private readonly IExerciseSettingsRepository _exerciseSettingsRepository;

    public ExerciseSettingsService(IExerciseSettingsRepository exerciseSettingsRepository)
    {
        _exerciseSettingsRepository = exerciseSettingsRepository;
    }

    public async Task<OperationResult<Exercise>> UpdateAsync(Guid userId, UpdateExerciseSettingModel updateModel,
        CancellationToken cancellationToken = default)
    {
        try
        {
            Exercise? updatedExercise =  await _exerciseSettingsRepository.UpdateAsync(userId, updateModel, cancellationToken);

            return updatedExercise != null
                ? OperationResult<Exercise>.Success(updatedExercise)
                : OperationResult<Exercise>.Failure(new[] { "Failed to update exercise settings." });
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception)
        {
            return OperationResult<Exercise>.Failure(new[] { "An unexpected error occurred while updating exercise settings." });
        }
    }

    public async Task<OperationResult<IEnumerable<Exercise>>> UpdateRangeAsync(Guid userId, IEnumerable<UpdateExerciseSettingModel> updateModels,
        CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<Exercise> updatedExercises = await _exerciseSettingsRepository.UpdateRangeAsync(userId, updateModels, cancellationToken);
            return OperationResult<IEnumerable<Exercise>>.Success(updatedExercises);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception)
        {
            return OperationResult<IEnumerable<Exercise>>.Failure(new[] { "An unexpected error occurred while updating exercise settings." });
        }
    }
}