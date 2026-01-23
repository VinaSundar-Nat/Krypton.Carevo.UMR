
namespace Kr.Carevo.UMR.Application.Feature.Application.Query.Fetch;

public sealed record ApplicationQuery(
    int ApplicationId
): IRequest<ApplicationResponseDto?>;
