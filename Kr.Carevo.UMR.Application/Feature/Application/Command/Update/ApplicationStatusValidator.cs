using FluentValidation;
using Kr.Carevo.UMR.Domain.Ports;

namespace Kr.Carevo.UMR.Application.Feature.Application.Command.Update;

public class ApplicationStatusValidator : AbstractValidator<ApplicationStatusCommand>
{
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationStatusValidator(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required")
            .GreaterThan(0).WithMessage("User ID must be greater than 0");

        RuleFor(x => x.ApplicationId)
            .NotEmpty().WithMessage("Application ID is required")
            .GreaterThan(0).WithMessage("Application ID must be greater than 0");

        RuleFor(x => x.StatusChange.Status)
            .NotNull().NotEmpty().WithMessage("Status is required")
            .Must(BeValidStatus).WithMessage("Invalid status value");

        // Verify application belongs to user
        RuleFor(x => new { x.UserId, x.ApplicationId })
            .MustAsync(async (data, cancellationToken) =>
            {
                return await _applicationRepository.IsApplicationOwnedByUserAsync(data.ApplicationId, data.UserId, cancellationToken);
            })
            .WithMessage("Application not found or does not belong to this user.");
    }

    private bool BeValidStatus(string status)
    {
        var validStatuses = new[] 
        { 
            "Applied", "UnderReview", "Shortlisted", "Interviewed", 
            "Accepted", "Rejected", "Withdrawn", "Saved", "Archived" 
        };
        
        return validStatuses.Contains(status, StringComparer.OrdinalIgnoreCase);
    }
}
