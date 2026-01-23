using Kr.Carevo.UMR.Domain.Ports;

namespace Kr.Carevo.UMR.Application.Feature.Application.Command.Create;

public class UserApplicationHandler(IApplicationRepository applicationRepository) : IRequestHandler<UserApplicationCommand, ApplicationResponseDto>
{
    public async Task<ApplicationResponseDto> Handle(UserApplicationCommand request, CancellationToken cancellationToken)
    {
        return await applicationRepository.CreateAsync(request.UserId, request.Application, cancellationToken);
    }
}
