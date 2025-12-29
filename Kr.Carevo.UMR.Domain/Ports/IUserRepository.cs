using Kr.Carevo.UMR.Domain.Dto;

namespace Kr.Carevo.UMR.Domain.Ports;

public interface IUserRepository
{
    Task<UserDto> Create(UserDto user);
}