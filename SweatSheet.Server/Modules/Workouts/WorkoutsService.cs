using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweatSheet.Server.Modules.Auth.Entities;
using SweatSheet.Server.Modules.Shared.Generics.PaginatedList;
using SweatSheet.Server.Modules.Workouts.DTOs;
using SweatSheet.Server.Modules.Workouts.Entities;

namespace SweatSheet.Server.Modules.Workouts;

public class WorkoutsService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public WorkoutsService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IResult> GetWorkouts(
        ClaimsPrincipal principal,
        UserManager<ApplicationUser> userManager,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10
    )
    {
        var userId = userManager.GetUserId(principal);

        var userWorkoutsQuery = _dbContext.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Workouts)
            .OrderBy(w => w.StartTime)
            .Include(w => w.Activities)
            .ThenInclude(a => a.Exercise)
            .AsNoTracking();

        var paginatedWorkouts = await PaginatedList<Workout>.ToPagedListAsync(userWorkoutsQuery, page, size);

        var workoutDtos = paginatedWorkouts.Map<Workout, WorkoutDto>(_mapper);

        return TypedResults.Ok(workoutDtos);
    }

    public async Task<IResult> GetSingleWorkout(
        ClaimsPrincipal principal,
        UserManager<ApplicationUser> userManager,
        [FromRoute] int id
    )
    {
        var userId = userManager.GetUserId(principal);

        var workout = await _dbContext.Workouts
            .Where(w => w.User.Id == userId)
            .Include(w => w.Activities)
            .ThenInclude(a => a.Exercise)
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Id == id);

        if (workout == null)
            return TypedResults.Forbid();

        var workoutDto = _mapper.Map<WorkoutDto>(workout);
        return TypedResults.Ok(workoutDto);
    }

    public async Task<IResult> CreateWorkout(
        ClaimsPrincipal principal,
        UserManager<ApplicationUser> userManager,
        WorkoutRequestDto workoutRequestDto
    )
    {
        var user = await userManager.GetUserAsync(principal);

        if (user == null)
        {
            return Results.Unauthorized();
        }

        var newWorkout = _mapper.Map<Workout>(workoutRequestDto);
        newWorkout.User = user;

        newWorkout.EndTime = DateTime.Now;

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

        var workoutDto = _mapper.Map<WorkoutDto>(newWorkout);

        return TypedResults.Ok(workoutDto);
    }

    public async Task<IResult> DeleteWorkout(
        ClaimsPrincipal principal,
        UserManager<ApplicationUser> userManager,
        [FromRoute] int id
    )
    {
        var userId = userManager.GetUserId(principal);

        var workoutToDelete = await _dbContext.Workouts
            .Where(w => w.User.Id == userId)
            .Include(w => w.Activities)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (workoutToDelete == null)
        {
            return TypedResults.Forbid();
        }

        _dbContext.Workouts.Remove(workoutToDelete);
        await _dbContext.SaveChangesAsync();

        return TypedResults.Ok();
    }

    public async Task<Results<Ok<Workout>, NotFound, ValidationProblem>> UpdateWorkout(
        ClaimsPrincipal principal,
        UserManager<ApplicationUser> userManager,
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
