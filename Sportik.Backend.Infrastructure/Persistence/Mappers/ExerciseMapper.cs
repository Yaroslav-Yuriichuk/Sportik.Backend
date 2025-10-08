using Sportik.Backend.Domain.Entities;
using Sportik.Backend.Infrastructure.Persistence.Entities;

namespace Sportik.Backend.Infrastructure.Persistence.Mappers;

internal static class ExerciseMapper
{
    public static Exercise ToDomain(UserExercise entity)
    {
        return new Exercise
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public static UserExercise ToEntity(Exercise domain, Guid userId)
    {
        return new UserExercise
        {
            Id = domain.Id,
            Name = domain.Name,
            UserId = userId
        };
    }
}