using SweatSheet.Server.Modules.Workouts.Entities;

namespace SweatSheet.Server.Modules.Workouts.DTOs;

public class WorkoutActivityDto
{
    public int Id { get; set; }
    public int? ExerciseId { get; set; }
    public List<ActivitySet> Sets { get; set; } = [];

    public double TotalWeightActivity
    {
        get { return Math.Round(Sets.Count != 0 ? Sets.Sum(s => s.Weight * s.Reps) : 0, 2); }
        set { }
    }
}