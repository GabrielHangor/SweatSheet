namespace SweatSheet.Server;

using System.ComponentModel.DataAnnotations;

public class Workout
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int WorkoutDurationInSeconds
    {
        get { return (int)(EndTime - StartTime).TotalSeconds; }
        set { }
    }

    public double TotalWeightWorkout
    {
        get { return Math.Round(Activities.Sum(a => a.TotalWeightActivity), 2); }
        set { }
    }

    public List<WorkoutActivity> Activities { get; set; } = [];
}
