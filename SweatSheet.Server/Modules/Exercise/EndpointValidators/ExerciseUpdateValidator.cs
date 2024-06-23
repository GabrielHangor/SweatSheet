using FluentValidation;
using SweatSheet.Server.Modules.Exercise.DTOs;

namespace SweatSheet.Server.Modules.Exercise.EndpointValidators;

public class ExerciseUpdateValidator : AbstractValidator<ExerciseUpdateRequestDto>
{
    public ExerciseUpdateValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(e => e).NotEmpty();

        RuleFor(e => e.ExerciseTitle).NotEmpty().WithMessage("Exercise title is required").MaximumLength(50);
        RuleFor(e => e.PrimaryMuscleGroup).NotEmpty().IsInEnum().WithMessage("Invalid muscle group");
    }
}
