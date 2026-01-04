



namespace Kr.Carevo.UMR.Domain.Ports;

public interface IEmploymentRepository
{
    Task<IEnumerable<EmploymentDto>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<ResponseDto> CreateUserEmploymentsAsync(int userId, IEnumerable<EmploymentDto> employments, CancellationToken cancellationToken = default);
}