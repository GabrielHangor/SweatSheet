namespace SweatSheet.Server;

public class WorkoutActivityRequestDto
{
    public int? ExerciseId { get; set; }
    public List<ActivitySet> Sets { get; set; } = [];
}
