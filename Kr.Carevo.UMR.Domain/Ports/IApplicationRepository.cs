namespace Kr.Carevo.UMR.Domain.Ports;

public interface IApplicationRepository
{
    Task<IEnumerable<ApplicationResponseDto>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<ApplicationResponseDto?> GetByIdAsync(int applicationId, CancellationToken cancellationToken = default);
    Task<ApplicationResponseDto> CreateAsync(int userId, ApplicationDto application, CancellationToken cancellationToken = default);
    Task<bool> ExistsByUserAndJobIdAsync(int userId, string jobId, CancellationToken cancellationToken = default);
    Task<bool> IsApplicationOwnedByUserAsync(int applicationId, int userId, CancellationToken cancellationToken = default);
    Task<bool> UpdateApplicationStatusAsync(int applicationId, int userId, ApplicationStatusDto statusChange, CancellationToken cancellationToken = default);
}
