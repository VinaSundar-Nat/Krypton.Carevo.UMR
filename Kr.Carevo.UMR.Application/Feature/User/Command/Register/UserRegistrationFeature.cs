using System;
using Kr.Carevo.UMR.Domain.Dto;
using Kr.Carevo.UMR.Domain.Ports;
using Kr.Common.Infrastructure.Resolver;

namespace Kr.Carevo.UMR.Application.Feature.User.Command.Register;

public class UserRegistrationFeature(IUserRepository repository) : IRequestHandler<UserRegistrationCommand, UserDto>
{
    public async Task<UserDto> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
    {
        return await repository.Create(request.User);
    }
}
