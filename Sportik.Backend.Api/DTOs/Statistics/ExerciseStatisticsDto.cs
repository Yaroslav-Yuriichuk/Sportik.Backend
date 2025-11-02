using Sportik.Backend.Api.DTOs.Exercises;

namespace Sportik.Backend.Api.DTOs.Statistics;

internal sealed record ExerciseStatisticsDto(ExerciseDto Exercise, List<SetDto> Sets);