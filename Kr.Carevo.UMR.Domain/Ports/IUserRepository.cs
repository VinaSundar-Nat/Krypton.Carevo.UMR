using Kr.Carevo.UMR.Domain.Dto;

namespace Kr.Carevo.UMR.Domain.Ports;

public interface IUserRepository
{
    Task<UserDto> CreateAsync(UserDto user, CancellationToken cancellationToken = default);
    Task<bool> ExistsByContactAsync(string contactValue, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<SkillDto>> UpdateSkillsAsync(int userId, IEnumerable<SkillDto> skills, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserResponseDto>> GetDetailsByIdAsync(int value, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserResponseDto>> GetDetailsByEmailAsync(string email, CancellationToken cancellationToken = default);
}