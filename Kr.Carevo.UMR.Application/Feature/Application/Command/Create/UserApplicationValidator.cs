using FluentValidation;
using Kr.Carevo.UMR.Domain.Ports;

namespace Kr.Carevo.UMR.Application.Feature.Application.Command.Create;

public class UserApplicationValidator : AbstractValidator<UserApplicationCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IApplicationRepository _applicationRepository;

    public UserApplicationValidator(IUserRepository userRepository, IApplicationRepository applicationRepository)
    {
        _userRepository = userRepository;
        _applicationRepository = applicationRepository;

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required")
            .GreaterThan(0).WithMessage("User ID must be greater than 0")
            .MustAsync(async (userId, cancellationToken) =>
            {
                return await _userRepository.ExistsByIdAsync(userId, cancellationToken);
            })
            .WithMessage("User does not exist.");

        RuleFor(x => x.Application)
            .NotNull().WithMessage("Application data is required");

        RuleFor(x => x.Application.JobId)
            .NotNull().NotEmpty().WithMessage("Job ID is required")
            .When(x => x.Application != null);

        RuleFor(x => x.Application.Status)
            .NotNull().NotEmpty().WithMessage("Status is required")
            .When(x => x.Application != null);

        RuleFor(x => x.Application.AppliedDate)
            .NotEqual(default(DateTime)).WithMessage("Applied date is required")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Applied date cannot be in the future")
            .When(x => x.Application != null);

        RuleFor(x => new { x.UserId, x.Application })
            .MustAsync(async (data, cancellationToken) =>
            {
                if (data.Application?.JobId == null)
                    return true;

                return !await _applicationRepository.ExistsByUserAndJobIdAsync(data.UserId, data.Application.JobId, cancellationToken);
            })
            .WithMessage("An application from this user for this job already exists.");
    }
}
