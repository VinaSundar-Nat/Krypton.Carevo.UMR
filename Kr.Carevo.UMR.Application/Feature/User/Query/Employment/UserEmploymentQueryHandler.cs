
using Kr.Carevo.UMR.Domain.Ports;

namespace Kr.Carevo.UMR.Application.Feature.User.Query.Employment;

public class UserEmploymentQueryHandler(IEmploymentRepository repository) : IRequestHandler<UserEmploymentQuery, IEnumerable<EmploymentResponseDto>>
{
    public async Task<IEnumerable<EmploymentResponseDto>> Handle(UserEmploymentQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetByUserIdAsync(request.UserId, cancellationToken);
    }
}