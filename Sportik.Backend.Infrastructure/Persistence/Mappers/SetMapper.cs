using Sportik.Backend.Domain.Models.Statistics;
using Sportik.Backend.Infrastructure.Persistence.Entities;

namespace Sportik.Backend.Infrastructure.Persistence.Mappers;

internal static class SetMapper
{
    public static Set ToDomain(UserSet entity, TimeSpan? offset = null)
    {
        return new Set
        {
            Id = entity.Id,
            Repetitions = entity.Repetitions,
            LoggedAt = offset != null ? entity.LoggedAt.ToOffset(offset.Value) : entity.LoggedAt,
            ExerciseId = entity.ExerciseId,
        };
    }

    public static UserSet ToEntity(Set domain, Guid userId)
    {
        return new UserSet
        {
            Id = domain.Id,
            Repetitions = domain.Repetitions,
            LoggedAt = domain.LoggedAt.ToUniversalTime(),
            UserId = userId,
            ExerciseId = domain.ExerciseId,
        };
    }
}