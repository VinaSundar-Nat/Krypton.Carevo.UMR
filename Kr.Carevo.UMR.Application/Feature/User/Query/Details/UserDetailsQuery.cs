
namespace Kr.Carevo.UMR.Application.Feature.User.Query.Details;

public enum LookupMode{
    ById,
    ByEmail
}


public sealed record UserDetailsQuery(
    LookupMode FilterMode,
    int? Id = null,
    string? Email  = null
): IRequest<IEnumerable<UserResponseDto>>;