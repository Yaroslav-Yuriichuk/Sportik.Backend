using Sportik.Backend.Application.Repositories.Interfaces;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Models;

namespace Sportik.Backend.Application.Services.Implementations;

internal sealed class ExercisesService : IExercisesService
{
    private readonly IExercisesRepository _exercisesRepository;

    public ExercisesService(IExercisesRepository exercisesRepository)
    {
        _exercisesRepository = exercisesRepository;
    }

    public async Task<IEnumerable<Exercise>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _exercisesRepository.GetAllAsync(userId, cancellationToken);
    }

    public async Task<Exercise?> GetByIdAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        return await _exercisesRepository.GetByIdAsync(userId, exerciseId, cancellationToken);
    }

    public async Task<Exercise> AddAsync(Guid userId, Exercise exercise, CancellationToken cancellationToken = default)
    {
        return await _exercisesRepository.AddAsync(userId, exercise, cancellationToken);
    }

    public async Task<Exercise?> DeleteAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        return await _exercisesRepository.DeleteAsync(userId, exerciseId, cancellationToken);
    }
}