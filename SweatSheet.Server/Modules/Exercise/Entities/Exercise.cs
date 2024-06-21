using System.ComponentModel.DataAnnotations;
using SweatSheet.Server.Modules.Exercise.Enums;

namespace SweatSheet.Server.Modules.Exercise.Entities
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
