using FluentValidation;
using Kr.Carevo.UMR.Domain.Ports;

namespace Kr.Carevo.UMR.Application.Feature.User.Command.Employment;

public class UserEmploymentCommandValidator : AbstractValidator<UserEmploymentCommand>
{
    private readonly IUserRepository _userRepository;

    public UserEmploymentCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required")
            .GreaterThan(0).WithMessage("User ID is invalid.")
            .MustAsync(async (userId, cancellationToken) =>
            {
                return await _userRepository.ExistsByIdAsync(userId, cancellationToken);
            })
            .WithMessage("User does not exist.");

        RuleFor(x => x.Employments)
            .NotNull().WithMessage("Employments cannot be null")
            .NotEmpty().WithMessage("At least one employment record is required");

        RuleForEach(x => x.Employments).ChildRules(employment =>
        {
            employment.RuleFor(e => e.Company)
                .NotNull().NotEmpty().WithMessage("Company name is required")
                .MaximumLength(200).WithMessage("Company name must not exceed 200 characters.");

            employment.RuleFor(e => e.StartDate)
                .NotEqual(default(DateTime)).WithMessage("Start date is required")
                .LessThan(DateTime.UtcNow).WithMessage("Start date cannot be in the future.");

            employment.RuleFor(e => e.EndDate)
                .GreaterThan(e => e.StartDate)
                .When(e => e.EndDate.HasValue)
                .WithMessage("End date must be after start date.");

            employment.RuleFor(e => e.Logo)
                .MaximumLength(500).WithMessage("Logo URL must not exceed 500 characters.")
                .When(e => !string.IsNullOrWhiteSpace(e.Logo));

            employment.RuleFor(e => e.Url)
                .MaximumLength(500).WithMessage("Company URL must not exceed 500 characters.")
                .When(e => !string.IsNullOrWhiteSpace(e.Url));
        });
    }
}
