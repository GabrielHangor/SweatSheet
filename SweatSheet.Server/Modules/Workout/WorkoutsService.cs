using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SweatSheet.Server;

public class WorkoutsService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public WorkoutsService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Ok<PaginatedList<Workout>>> GetWorkouts(
        [FromQuery] int page = 1,
        [FromQuery] int size = 10
    )
    {
        var workouts = await PaginatedList<Workout>.ToPagedList(
            _dbContext.Workouts
                .AsNoTracking()
                .OrderBy(w => w.StartTime)
                .Include(w => w.Activities)
                .ThenInclude(a => a.Exercise),
            page,
            size
        );

        return TypedResults.Ok(workouts);
    }

    public async Task<Results<Ok<Workout>, NotFound>> GetSingleWorkout([FromRoute] int id)
    {
        var workout = await _dbContext.Workouts
            .AsNoTracking()
            .Include(w => w.Activities)
            .ThenInclude(a => a.Exercise)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (workout != null)
        {
            return TypedResults.Ok(workout);
        }

        return TypedResults.NotFound();
    }

    public async Task<Results<Ok<Workout>, ValidationProblem>> CreateWorkout(
        WorkoutRequestDto workoutRequestDto
    )
    {
        var newWorkout = _mapper.Map<Workout>(workoutRequestDto);

        newWorkout.EndTime = new DateTime();

        foreach (var activityDto in workoutRequestDto.Activities)
        {
            var exercise =
                await _dbContext.Exercises.FindAsync(activityDto.ExerciseId)
                ?? throw new InvalidOperationException("Exercise not found.");

            var activity = _mapper.Map<WorkoutActivity>(activityDto);
            activity.Exercise = exercise;
            newWorkout.Activities.Add(activity);
        }

        _dbContext.Workouts.Add(newWorkout);
        await _dbContext.SaveChangesAsync();

        return TypedResults.Ok(newWorkout);
    }

    public async Task<Results<Ok, NotFound>> DeleteWorkout([FromRoute] int id)
    {
        var workoutToDelete = await _dbContext.Workouts
            .Include(w => w.Activities)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (workoutToDelete == null)
        {
            return TypedResults.NotFound();
        }

        _dbContext.Workouts.Remove(workoutToDelete);
        await _dbContext.SaveChangesAsync();

        return TypedResults.Ok();
    }

    public async Task<Results<Ok<Workout>, NotFound, ValidationProblem>> UpdateWorkout(
        [FromRoute] int id,
        WorkoutRequestDto workoutDto
    )
    {
        var workoutToUpdate = await _dbContext.Workouts
            .Include(w => w.Activities)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (workoutToUpdate == null)
        {
            return TypedResults.NotFound();
        }

        workoutToUpdate.Activities.Clear();

        workoutToUpdate.Title = workoutDto.Title;
        workoutToUpdate.StartTime = workoutDto.StartTime;
        workoutToUpdate.EndTime = workoutDto.EndTime;

        foreach (var activityDto in workoutDto.Activities)
        {
            var exercise =
                await _dbContext.Exercises.FindAsync(activityDto.ExerciseId)
                ?? throw new InvalidOperationException("Exercise not found.");

            var activity = _mapper.Map<WorkoutActivity>(activityDto);

            activity.Exercise = exercise;

            workoutToUpdate.Activities.Add(activity);
        }

        await _dbContext.SaveChangesAsync();

        return TypedResults.Ok(workoutToUpdate);
    }
}
