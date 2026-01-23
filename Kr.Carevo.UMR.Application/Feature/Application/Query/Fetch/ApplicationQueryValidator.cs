using FluentValidation;

namespace Kr.Carevo.UMR.Application.Feature.Application.Query.Fetch;

public class ApplicationQueryValidator : AbstractValidator<ApplicationQuery>
{
    public ApplicationQueryValidator()
    {
        RuleFor(x => x.ApplicationId)
            .NotEmpty().WithMessage("Application ID is required")
            .GreaterThan(0).WithMessage("Application ID must be greater than 0");
    }
}
