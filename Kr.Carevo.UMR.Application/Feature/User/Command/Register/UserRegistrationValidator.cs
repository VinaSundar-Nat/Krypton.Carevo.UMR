

using FluentValidation;

namespace Kr.Carevo.UMR.Application.Feature.User.Command.Register;


// Validator for your command
public class UserRegistrationCommandValidator : AbstractValidator<UserRegistrationCommand>
{
    public UserRegistrationCommandValidator()
    {
        RuleFor(x => x.User.FirstName)
            .NotEmpty().WithMessage("First name is required");

        RuleFor(x => x.User.LastName)
            .NotEmpty().WithMessage("Last name is required");

        RuleFor(x => x.User.Dob)
            .NotEmpty().WithMessage("Date of birth is required");

        RuleFor(x => x.User.Contacts)
            .NotEmpty().WithMessage("Contact is required");

        RuleForEach(x => x.User.Contacts)
            .Must(contact => contact.IsValid)
            .WithMessage("One or more contacts are invalid");
    }
}