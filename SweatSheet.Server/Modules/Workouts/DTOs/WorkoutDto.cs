using SweatSheet.Server.Modules.Workouts.Entities;

namespace SweatSheet.Server.Modules.Workouts.DTOs;

public class WorkoutDto {
    public int Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int WorkoutDurationInSeconds { get; set; }
    public double TotalWeightWorkout { get; set; }
    public List<WorkoutActivityDto> Activities { get; init; } = [];
}