namespace SweatSheet.Server;

public class ExerciseCreateRequestDto
{
    public MuscleGroup? PrimaryMuscleGroup { get; set; }
    public string ExerciseTitle { get; set; } = string.Empty;
}

public class ExerciseUpdateRequestDto : ExerciseCreateRequestDto;
