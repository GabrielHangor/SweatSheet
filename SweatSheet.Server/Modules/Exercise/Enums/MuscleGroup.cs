using System.ComponentModel.DataAnnotations;

namespace SweatSheet.Server;

public enum MuscleGroup
{
    [Display(Name = "Chest")]
    Chest,

    [Display(Name = "Back")]
    Back,

    [Display(Name = "Shoulders")]
    Shoulders,

    [Display(Name = "Biceps")]
    Biceps,

    [Display(Name = "Triceps")]
    Triceps,

    [Display(Name = "Legs")]
    Legs
}

public static class MuscleGroupExtensions
{
    public static string GetDisplayName(this MuscleGroup muscleGroup)
    {
        var memberInfo = typeof(MuscleGroup).GetMember(muscleGroup.ToString());
        var displayAttribute = (DisplayAttribute)
            memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false)[0];

        return displayAttribute.Name!;
    }
}
