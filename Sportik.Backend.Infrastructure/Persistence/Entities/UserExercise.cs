using Sportik.Backend.Infrastructure.Persistence.Entities.Identity;

namespace Sportik.Backend.Infrastructure.Persistence.Entities;

internal sealed class UserExercise
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; } = null!;

    public Guid UserId { get; init; }

    public ApplicationUser User { get; init; } = null!;

    public UserExerciseSettings Settings { get; init; } = null!;
}