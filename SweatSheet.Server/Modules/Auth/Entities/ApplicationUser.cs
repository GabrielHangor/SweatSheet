using Microsoft.AspNetCore.Identity;
using SweatSheet.Server.Modules.Workouts.Entities;

namespace SweatSheet.Server.Modules.Auth.Entities;

public class ApplicationUser : IdentityUser {
    public List<Workout> Workouts { get; init; } = [];
}

public class ApplicationRole : IdentityRole { }