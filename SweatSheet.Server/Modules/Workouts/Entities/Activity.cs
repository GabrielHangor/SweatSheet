using System.ComponentModel.DataAnnotations;

namespace SweatSheet.Server.Modules.Workouts.Entities;

public class ActivitySet
{
    public double Weight { get; set; }
    public int Reps { get; set; }
}

public class WorkoutActivity
{
    [Key]
    public int Id { get; set; }

    public Exercise.Entities.Exercise? Exercise { get; set; }

    public List<ActivitySet> Sets { get; set; } = [];

    public Workout Workout { get; set; }

    public double TotalWeightActivity
    {
        get { return Math.Round(Sets.Count != 0 ? Sets.Sum(s => s.Weight * s.Reps) : 0, 2); }
        set { }
    }
}

