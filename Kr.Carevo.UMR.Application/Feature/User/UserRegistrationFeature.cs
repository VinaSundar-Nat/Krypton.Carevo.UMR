using System;
using Kr.Carevo.UMR.Domain.Dto;
using Kr.Carevo.UMR.Domain.Ports;

namespace Kr.Carevo.UMR.Application.Feature.User;

public class UserRegistrationFeature(IUserRepository repository) : IUserRegistrationFeature
{
    public async Task<UserDto> Register(UserDto user)
    {
        return await repository.Create(user);
    }
}
