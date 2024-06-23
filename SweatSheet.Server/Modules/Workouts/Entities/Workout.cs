using System.ComponentModel.DataAnnotations;
using SweatSheet.Server.Modules.Auth.Entities;

namespace SweatSheet.Server.Modules.Workouts.Entities;

public class Workout
{
    [Key]
    public int Id { get; init; }

    [MaxLength (100)]
    public string Title { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int WorkoutDurationInSeconds => (int)(EndTime - StartTime).TotalSeconds;

    public double TotalWeightWorkout
    {
        get { return Math.Round(Activities.Sum(a => a.TotalWeightActivity), 2); }

    }

    public List<WorkoutActivity> Activities { get; init; } = [];

    public  ApplicationUser User { get; set; }
}