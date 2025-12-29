using Kr.Carevo.UMR.Domain.Dto;

namespace Kr.Carevo.UMR.Domain.Ports;

public interface IUserRegistrationFeature
{
    Task<UserDto> Register(UserDto user);
}
