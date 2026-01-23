
namespace Kr.Carevo.UMR.Application.Feature.Application.Query.UserApplications;

public sealed record UserApplicationsQuery(
    int UserId
): IRequest<IEnumerable<ApplicationResponseDto>>;