using FluentValidation;

namespace Kr.Carevo.UMR.Application.Feature.User.Command.Skills;

public class UserSkillsCommandValidator : AbstractValidator<UserSkillsCommand>
{
    public UserSkillsCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull()
            .NotEmpty().WithMessage("User ID is required");

        RuleFor(x => x.Skills)
            .NotEmpty().WithMessage("Skills are required");

        RuleForEach(x => x.Skills).ChildRules(skill =>
        {
            skill.RuleFor(s => s.Code).NotNull().NotEmpty().WithMessage("Skill code is required.")
                .MaximumLength(50).WithMessage("Skill code must not exceed 50 characters.");

            skill.RuleFor(s => s.Description).NotNull().NotEmpty().WithMessage("Skill description is required.")
                .MaximumLength(200).WithMessage("Skill description must not exceed 200 characters.");

            skill.RuleFor(s => s.EffectiveDate)
                .NotEqual(default(DateTime)).WithMessage("Skill effective date is required.")
                .When(s => s.EffectiveDate.HasValue)
               .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Skill effective date cannot be in the future.");
        });
    }
}