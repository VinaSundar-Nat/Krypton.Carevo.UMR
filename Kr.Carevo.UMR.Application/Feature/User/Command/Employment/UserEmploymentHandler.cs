using System;
using Kr.Carevo.UMR.Domain.Dto;
using Kr.Carevo.UMR.Domain.Ports;
using Kr.Common.Mediator;

namespace Kr.Carevo.UMR.Application.Feature.User.Command.Employment;

public class UserEmploymentHandler(IEmploymentRepository employmentRepository) : IRequestHandler<UserEmploymentCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UserEmploymentCommand request, CancellationToken cancellationToken)
    {
        return await employmentRepository.CreateUserEmploymentsAsync(request.UserId, request.Employments, cancellationToken);
    }
}