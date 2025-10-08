using Sportik.Backend.Application.Repositories.Interfaces;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Entities;

namespace Sportik.Backend.Application.Services.Implementations;

internal sealed class ExercisesService : IExercisesService
{
    private readonly IExercisesRepository _exercisesRepository;

    public ExercisesService(IExercisesRepository exercisesRepository)
    {
        _exercisesRepository = exercisesRepository;
    }

    public async Task<IEnumerable<Exercise>> GetUserExercisesAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _exercisesRepository.GetUserExercisesAsync(userId, cancellationToken);
    }

    public async Task<Exercise?> GetUserExerciseByIdAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        return await _exercisesRepository.GetUserExerciseByIdAsync(userId, exerciseId, cancellationToken);
    }

    public async Task<Exercise> AddUserExerciseAsync(Guid userId, Exercise exercise, CancellationToken cancellationToken = default)
    {
        return await _exercisesRepository.AddUserExerciseAsync(userId, exercise, cancellationToken);
    }

    public async Task<Exercise?> DeleteUserExerciseAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        return await _exercisesRepository.DeleteUserExerciseAsync(userId, exerciseId, cancellationToken);
    }
}