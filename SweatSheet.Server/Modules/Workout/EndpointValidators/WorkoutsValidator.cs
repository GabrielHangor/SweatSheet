using FluentValidation;

namespace SweatSheet.Server;

public class WorkoutValidator : AbstractValidator<WorkoutRequestDto>
{
    public WorkoutValidator(AppDbContext dbContext)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(w => w.Title)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Title must be between 1 and 50 characters");

        RuleFor(w => w.StartTime)
            .NotEmpty()
            .LessThan(w => w.EndTime)
            .WithMessage("Start time must be before end time");

        RuleFor(w => w.EndTime)
            .NotEmpty()
            .GreaterThan(w => w.StartTime)
            .WithMessage("End time must be after start time");

        RuleForEach(w => w.Activities).SetValidator(new ActivityValidator(dbContext));
    }
}
