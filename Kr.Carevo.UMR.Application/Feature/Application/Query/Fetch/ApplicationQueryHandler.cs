using Kr.Carevo.UMR.Domain.Ports;

namespace Kr.Carevo.UMR.Application.Feature.Application.Query.Fetch;

public class ApplicationQueryHandler(IApplicationRepository repository) 
    : IRequestHandler<ApplicationQuery, ApplicationResponseDto?>
{
    public async Task<ApplicationResponseDto?> Handle(ApplicationQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetByIdAsync(request.ApplicationId, cancellationToken);
    }
}
