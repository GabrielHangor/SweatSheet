using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SweatSheet.Server.Modules.Workouts.DTOs;

namespace SweatSheet.Server.Modules.Workouts.EndpointValidators;

public class ActivityValidator : AbstractValidator<WorkoutActivityRequestDto>
{
    public ActivityValidator(AppDbContext dbContext)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(a => a.ExerciseId)
            .NotEmpty()
            .WithMessage("Exercise is not selected")
            .MustAsync(
                async (exerciseId, cancellationToken) =>
                    await dbContext.Exercises.AnyAsync(e => e.Id == exerciseId, cancellationToken)
            )
            .WithMessage("Invalid exercise id");

        RuleForEach(a => a.Sets)
            .ChildRules(setsRules =>
            {
                setsRules.RuleFor(s => s.Weight).GreaterThan(0).WithMessage("Weight must be greater than 0");

                setsRules.RuleFor(s => s.Reps).GreaterThan(0).WithMessage("Reps must be greater than 0");
            });
    }
}
