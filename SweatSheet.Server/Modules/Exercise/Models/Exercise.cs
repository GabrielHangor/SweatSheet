using System.ComponentModel.DataAnnotations;

namespace SweatSheet.Server
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }
        public MuscleGroup? PrimaryMuscleGroup { get; set; }
        public string PrimaryMuscleGroupTitle => PrimaryMuscleGroup?.GetDisplayName() ?? string.Empty;
        public string ExerciseTitle { get; set; } = string.Empty;
    }
}
