using Sportik.Backend.Domain.Entities;

namespace Sportik.Backend.Application.Repositories.Interfaces;

public interface IExercisesRepository
{
    Task<IEnumerable<Exercise>> GetUserExercisesAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<Exercise?> GetUserExerciseByIdAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);

    Task<Exercise> AddUserExerciseAsync(Guid userId, Exercise exercise, CancellationToken cancellationToken = default);

    Task<Exercise?> DeleteUserExerciseAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
}