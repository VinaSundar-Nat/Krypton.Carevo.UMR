using FluentValidation;

namespace Kr.Carevo.UMR.Application.Feature.User.Query.Employment;

public class UserEmploymentQueryValidator : AbstractValidator<UserEmploymentQuery>
{
    public UserEmploymentQueryValidator()
    {
        RuleFor(x => x.UserId).NotNull().GreaterThan(0)
            .NotEmpty().WithMessage("User ID is required");      
    }
}