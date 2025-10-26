namespace Sportik.Backend.Api.DTOs.Settings;

public sealed record ExerciseSettingsUpdateDto(Guid ExerciseId, ExerciseSettingsDeltaDto Delta);