using FluentValidation;
using Kr.Carevo.UMR.Domain.Ports;

namespace Kr.Carevo.UMR.Application.Feature.Application.Query.UserApplications;

public class UserApplicationsQueryValidator : AbstractValidator<UserApplicationsQuery>
{
    private readonly IUserRepository _userRepository;

    public UserApplicationsQueryValidator(IUserRepository userRepository)
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
    }
}
