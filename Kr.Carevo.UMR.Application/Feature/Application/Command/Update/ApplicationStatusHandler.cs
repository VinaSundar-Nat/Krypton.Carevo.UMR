using Kr.Carevo.UMR.Domain.Ports;

namespace Kr.Carevo.UMR.Application.Feature.Application.Command.Update;

public class ApplicationStatusHandler(IApplicationRepository applicationRepository) : IRequestHandler<ApplicationStatusCommand, bool>
{
    public async Task<bool> Handle(ApplicationStatusCommand request, CancellationToken cancellationToken)
    {
        return await applicationRepository.UpdateApplicationStatusAsync(
            request.ApplicationId, 
            request.UserId, 
            request.StatusChange, 
            cancellationToken);
    }
}
