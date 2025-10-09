using Sportik.Backend.Api.DTOs.Exercises;
using Sportik.Backend.Domain.Entities;

namespace Sportik.Backend.Api.Mappers;

internal static class ExerciseMapper
{
    public static ExerciseDto ToDto(Exercise exercise)
    {
        return new ExerciseDto(
            Id: exercise.Id,
            Name: exercise.Name,
            Settings: ExerciseSettingsMapper.ToDto(exercise.Settings));
    }

    public static Exercise ToDomain(ExerciseDto dto)
    {
        return new Exercise
        {
            Id = dto.Id,
            Name = dto.Name,
            Settings = ExerciseSettingsMapper.ToDomain(dto.Settings),
        };
    }

    public static Exercise ToDomain(AddExerciseDto dto)
    {
        return new Exercise
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Settings = ExerciseSettingsMapper.ToDomain(dto.Settings),
        };
    }
}