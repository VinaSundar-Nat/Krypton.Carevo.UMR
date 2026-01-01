

using FluentValidation;
using Kr.Carevo.UMR.Domain.Ports;

namespace Kr.Carevo.UMR.Application.Feature.User.Command.Register;


// Validator for your command
public class UserRegistrationCommandValidator : AbstractValidator<UserRegistrationCommand>
{
    private readonly IUserRepository _userRepository;

    public UserRegistrationCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.User.FirstName)
            .NotEmpty().WithMessage("First name is required");

        RuleFor(x => x.User.LastName)
            .NotEmpty().WithMessage("Last name is required");

        RuleFor(x => x.User.Dob)
            .NotEmpty().WithMessage("Date of birth is required");

        RuleFor(x => x.User.Contact)
            .NotEmpty().WithMessage("Contact is required");

        RuleFor(x => x.User.Contact)
            .Must(contact => contact.IsValid)
            .WithMessage("An email or phone number must be provided in contact information.");

        RuleFor(x => x.User.Contact.Email)
            .MustAsync(async (email, cancellationToken) =>
            {
                if (string.IsNullOrWhiteSpace(email))
                    return true;

                return !await _userRepository.ExistsByContactAsync(email, cancellationToken);
            })
            .WithMessage("This email is already registered.")
            .When(x => !string.IsNullOrWhiteSpace(x.User.Contact.Email));
    }
}