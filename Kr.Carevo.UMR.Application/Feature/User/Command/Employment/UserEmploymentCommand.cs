
using Kr.Carevo.UMR.Domain.Dto;
using Kr.Common.Mediator;

namespace Kr.Carevo.UMR.Application.Feature.User.Command.Employment;

public sealed class UserEmploymentCommand : IRequest<ResponseDto>
{
    public required int UserId { get; init; }
    public required IEnumerable<EmploymentDto> Employments { get; init; }
}