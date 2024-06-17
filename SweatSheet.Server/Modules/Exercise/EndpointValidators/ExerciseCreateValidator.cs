using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace SweatSheet.Server;

public class ExerciseCreateValidator : AbstractValidator<ExerciseCreateRequestDto>
{
    public ExerciseCreateValidator(AppDbContext dbContext)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(e => e)
            .NotEmpty()
            .MustAsync(async (exercise, cancellationToken) => !await IsDuplicate(exercise, dbContext))
            .WithMessage("Duplicate exercise").WithName("exercise");

        RuleFor(e => e.ExerciseTitle).NotEmpty().WithMessage("Exercise title is required").MaximumLength(50);
        RuleFor(e => e.PrimaryMuscleGroup).NotEmpty().IsInEnum().WithMessage("Invalid muscle group");
    }

    private static async Task<bool> IsDuplicate(ExerciseCreateRequestDto exercise, AppDbContext dbContext)
    {
        return await dbContext.Exercises.AnyAsync(
            e => e.ExerciseTitle == exercise.ExerciseTitle && e.PrimaryMuscleGroup == exercise.PrimaryMuscleGroup
        );
    }
}
