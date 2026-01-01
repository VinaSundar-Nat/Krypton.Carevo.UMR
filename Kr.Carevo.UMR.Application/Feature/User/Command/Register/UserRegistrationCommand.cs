

using Kr.Carevo.UMR.Domain.Dto;
using Kr.Common.Mediator;

namespace Kr.Carevo.UMR.Application.Feature.User.Command.Register;

public sealed class UserRegistrationCommand : IRequest<UserDto>
{
    public required UserDto User { get; init; }
}