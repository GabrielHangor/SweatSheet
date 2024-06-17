namespace SweatSheet.Server;

public static class WorkoutEndpoints
{
    public static void RegisterWorkoutEndpoints(this WebApplication app)
    {
        var workoutsGroup = app.MapGroup("/api/v1/workouts").WithTags("Workouts");

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
