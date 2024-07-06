using Microsoft.AspNetCore.Identity;
using SweatSheet.Server.Modules.Workouts.Entities;

{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Workout>? Workouts { get; set; }
    }
}

public class ApplicationRole : IdentityRole { }
