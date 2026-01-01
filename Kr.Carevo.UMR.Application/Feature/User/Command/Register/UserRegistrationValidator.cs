using FluentValidation;
using Kr.Carevo.UMR.Domain.Ports;

namespace Kr.Carevo.UMR.Application.Feature.User.Command.Register;


public class UserRegistrationCommandValidator : AbstractValidator<UserRegistrationCommand>
{
    private readonly IUserRepository _userRepository;

    public UserRegistrationCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.User.FirstName).NotNull()
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(200).WithMessage("First name must not exceed 200 characters.");

        RuleFor(x => x.User.LastName).NotNull()
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(200).WithMessage("Last name must not exceed 200 characters.");

        RuleFor(x => x.User.Dob).NotNull().NotEmpty()
            .NotEqual(default(DateTime))
            .WithMessage("Date of birth is required")
            .LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past.")
            .GreaterThan(DateTime.UtcNow.AddYears(-80)).WithMessage("Date of birth is not valid.");

        RuleFor(x => x.User.Contact)
            .NotEmpty().WithMessage("Contact is required");

        RuleFor(x => x.User.Contact)
            .Must(contact => contact.IsValid)
            .WithMessage("An email or phone number must be provided in contact information.");

        RuleFor(x => x.User.Contact.Email).NotNull()
            .NotEmpty().WithMessage("Email is required")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .When(x => !string.IsNullOrWhiteSpace(x.User.Contact.Email))
            .WithMessage("Email format is invalid.")
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