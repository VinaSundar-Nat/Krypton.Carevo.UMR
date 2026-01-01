using FluentValidation;

namespace Kr.Carevo.UMR.Application.Feature.User.Query.Details;

public class UserDetailsQueryValidator : AbstractValidator<UserDetailsQuery>
{
    public UserDetailsQueryValidator()
    {
        RuleFor(x => x.Id).NotNull()
            .NotEmpty().WithMessage("User ID is required")
            .When(x => x.FilterMode == LookupMode.ById);

        RuleFor(x => x.Email).NotNull()
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email format is invalid.")
            .When(x => x.FilterMode == LookupMode.ByEmail);         
    }
}