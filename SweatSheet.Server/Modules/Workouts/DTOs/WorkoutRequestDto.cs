namespace SweatSheet.Server.Modules.Workouts.DTOs;

public class WorkoutRequestDto
{
    public string Title { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<WorkoutActivityRequestDto> Activities { get; set; } = [];
}
