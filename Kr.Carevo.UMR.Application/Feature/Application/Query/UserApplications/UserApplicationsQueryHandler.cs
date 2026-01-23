using Kr.Carevo.UMR.Domain.Ports;

namespace Kr.Carevo.UMR.Application.Feature.Application.Query.UserApplications;

public class UserApplicationsQueryHandler(IApplicationRepository repository) 
    : IRequestHandler<UserApplicationsQuery, IEnumerable<ApplicationResponseDto>>
{
    public async Task<IEnumerable<ApplicationResponseDto>> Handle(UserApplicationsQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetByUserIdAsync(request.UserId, cancellationToken);
    }
}
