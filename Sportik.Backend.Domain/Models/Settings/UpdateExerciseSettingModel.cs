namespace Sportik.Backend.Domain.Models.Settings;

public sealed class UpdateExerciseSettingModel
{
    public Guid ExerciseId { get; init; }

    public ExerciseSettingsDelta Delta { get; init; }
}