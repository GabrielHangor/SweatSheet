using SweatSheet.Server.Modules.Shared.Generics;
using SweatSheet.Server.Modules.Workouts.DTOs;

namespace SweatSheet.Server.Modules.Workouts;

public static class WorkoutEndpoints
{
    public static void RegisterWorkoutEndpoints(this WebApplication app)
    {
        var workoutsGroup = app.MapGroup("/api/v1/workouts").WithTags("Workouts").RequireAuthorization();

        var workoutsService = app.Services.GetRequiredService<WorkoutsService>();

        workoutsGroup.MapGet("/", workoutsService.GetWorkouts);

        workoutsGroup.MapGet("/{id:int}", workoutsService.GetSingleWorkout);

        workoutsGroup
            .MapPost("/", workoutsService.CreateWorkout)
            .AddEndpointFilter<ValidationFilter<WorkoutRequestDto>>();

        workoutsGroup.MapDelete("/{id:int}", workoutsService.DeleteWorkout);

        workoutsGroup
            .MapPut("/{id:int}", workoutsService.UpdateWorkout)
            .AddEndpointFilter<ValidationFilter<WorkoutRequestDto>>();
        ;
    }
}
