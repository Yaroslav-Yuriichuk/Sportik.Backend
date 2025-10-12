using Sportik.Backend.Infrastructure.Persistence.Entities.Identity;

namespace Sportik.Backend.Infrastructure.Persistence.Entities;

internal sealed class UserSet
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public int Repetitions { get; init; }

    public DateTimeOffset LoggedAt { get; init; }

    public Guid ExerciseId { get; init; }

    public UserExercise Exercise { get; init; } = null!;

    public Guid UserId { get; init; }

    public ApplicationUser User { get; init; } = null!;
}