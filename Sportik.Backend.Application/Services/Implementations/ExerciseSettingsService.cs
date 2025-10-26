using Sportik.Backend.Application.Repositories.Interfaces;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Models.Settings;

namespace Sportik.Backend.Application.Services.Implementations;

internal sealed class ExerciseSettingsService : IExerciseSettingsService
{
    private readonly IExerciseSettingsRepository _exerciseSettingsRepository;

    public ExerciseSettingsService(IExerciseSettingsRepository exerciseSettingsRepository)
    {
        _exerciseSettingsRepository = exerciseSettingsRepository;
    }

    public async Task<OperationResult<ExerciseSettings>> UpdateAsync(Guid userId, ExerciseSettingsDelta delta, Guid exerciseId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ExerciseSettings? updatedSettings =  await _exerciseSettingsRepository.UpdateAsync(userId, delta, exerciseId, cancellationToken);

            return updatedSettings != null
                ? OperationResult<ExerciseSettings>.Success(updatedSettings)
                : OperationResult<ExerciseSettings>.Failure(new[] { "Failed to update exercise settings." });
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception)
        {
            return OperationResult<ExerciseSettings>.Failure(new[] { "An unexpected error occurred while updating exercise settings." });
        }
    }
}