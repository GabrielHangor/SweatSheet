using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SweatSheet.Server;

public class ExercisesService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public ExercisesService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Ok<List<Exercise>>> GetExercises()
    {
        var exercises = await _dbContext.Exercises.ToListAsync();

        return TypedResults.Ok(exercises);
    }

    public async Task<Results<Ok<Exercise>, ValidationProblem>> CreateExercise(
        ExerciseCreateRequestDto exercisetDto
    )
    {
        var newExercise = _mapper.Map<Exercise>(exercisetDto);

        _dbContext.Exercises.Add(newExercise);

        await _dbContext.SaveChangesAsync();

        return TypedResults.Ok(newExercise);
    }

    public async Task<Results<Ok, NotFound<string>>> DeleteExercise([FromRoute] int id)
    {
        var exerciseToDetele = await _dbContext.Exercises.FirstOrDefaultAsync(e => e.Id == id);

        if (exerciseToDetele != null)
        {
            _dbContext.Exercises.Remove(exerciseToDetele);
            await _dbContext.SaveChangesAsync();

            return TypedResults.Ok();
        }

        return TypedResults.NotFound("Exercise not found");
    }

    public async Task<Results<Ok<Exercise>, NotFound<string>, ValidationProblem>> UpdateExercise(
        [FromRoute] int id,
        ExerciseUpdateRequestDto exerciseDto
    )
    {
        var exerciseToUpdate = await _dbContext.Exercises.FirstOrDefaultAsync(e => e.Id == id);

        if (exerciseToUpdate != null)
        {
            exerciseToUpdate.ExerciseTitle = exerciseDto.ExerciseTitle;
            exerciseToUpdate.PrimaryMuscleGroup = exerciseDto.PrimaryMuscleGroup;

            await _dbContext.SaveChangesAsync();

            return TypedResults.Ok(exerciseToUpdate);
        }

        return TypedResults.NotFound("Exercise not found");
    }

    public async Task<Results<Ok<double>, NotFound<string>>> GetAverageTotal([FromRoute] int id)
    {
        var isValidExercise = await _dbContext.Exercises.AnyAsync(e => e.Id == id);

        if (isValidExercise)
        {
            var averageTotal = await _dbContext.Activities
                .AsNoTracking()
                .Where(a => a.Exercise != null && a.Exercise.Id == id)
                .Select(a => a.TotalWeightActivity)
                .AverageAsync();

            return TypedResults.Ok(averageTotal);
        }

        return TypedResults.NotFound("Exercise not found");
    }
}
