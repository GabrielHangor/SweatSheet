using SweatSheet.Server.Modules.Exercise.DTOs;
using SweatSheet.Server.Modules.Shared.Generics;

namespace SweatSheet.Server.Modules.Exercise;

public static class ExercisesEndpoints
{
    public static void RegisterExerciseEndpoints(this WebApplication app)
    {
        var exercisesGroup = app.MapGroup("/api/v1/exercises").WithTags("Exercises");

        var exercisesService = app.Services.GetRequiredService<ExercisesService>();

        exercisesGroup.MapGet("/", exercisesService.GetExercises);

        exercisesGroup.MapGet("/average/{id:int}", exercisesService.GetAverageTotal);

        exercisesGroup
            .MapPost("/", exercisesService.CreateExercise)
            .AddEndpointFilter<ValidationFilter<ExerciseCreateRequestDto>>();

        exercisesGroup.MapDelete("/{id:int}", exercisesService.DeleteExercise);

        exercisesGroup
            .MapPut("/{id:int}", exercisesService.UpdateExercise)
            .AddEndpointFilter<ValidationFilter<ExerciseUpdateRequestDto>>();
    }
}
