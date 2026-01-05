

namespace Kr.Carevo.UMR.Application.Feature.User.Query.Employment;

public sealed record UserEmploymentQuery(
    int UserId
): IRequest<IEnumerable<EmploymentResponseDto>>;