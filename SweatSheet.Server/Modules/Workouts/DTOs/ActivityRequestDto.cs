using SweatSheet.Server.Modules.Workouts.Entities;

namespace SweatSheet.Server.Modules.Workouts.DTOs;

public class WorkoutActivityRequestDto
{
    public int? ExerciseId { get; set; }
    public List<ActivitySet> Sets { get; set; } = [];
}
