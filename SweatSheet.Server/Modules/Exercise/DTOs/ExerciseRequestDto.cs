using SweatSheet.Server.Modules.Exercise.Enums;

namespace SweatSheet.Server.Modules.Exercise.DTOs;

public class ExerciseCreateRequestDto
{
    public MuscleGroup? PrimaryMuscleGroup { get; set; }
    public string ExerciseTitle { get; set; } = string.Empty;
}

public class ExerciseUpdateRequestDto : ExerciseCreateRequestDto;
