using Sportik.Backend.Api.DTOs.Statistics;
using Sportik.Backend.Domain.Models.Statistics;

namespace Sportik.Backend.Api.Mappers.Statistics;

internal static class SetMapper
{
    public static SetDto ToDto(Set domain)
    {
        return new SetDto(
            Id: domain.Id,
            Repetitions: domain.Repetitions,
            LoggedAt: domain.LoggedAt);
    }

    public static Set ToDomain(AddSetDto dto)
    {
        return new Set
        {
            Id = dto.Id ?? Guid.NewGuid(),
            Repetitions = dto.Repetitions,
            LoggedAt = dto.LoggedAt,
            ExerciseId = dto.ExerciseId,
        };
    }
}