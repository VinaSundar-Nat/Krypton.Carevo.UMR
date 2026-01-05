

namespace Kr.Carevo.UMR.Application.Feature.User.Command.Register;

public sealed class UserRegistrationCommand : IRequest<UserDto>
{
    public required UserDto User { get; init; }
}