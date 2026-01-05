using Kr.Carevo.UMR.Domain.Ports;

namespace Kr.Carevo.UMR.Application.Feature.User.Command.Employment;

public class UserEmploymentHandler(IEmploymentRepository employmentRepository) : IRequestHandler<UserEmploymentCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UserEmploymentCommand request, CancellationToken cancellationToken)
    {
        return await employmentRepository.CreateUserEmploymentsAsync(request.UserId, request.Employments, cancellationToken);
    }
}