
using Kr.Carevo.UMR.Domain.Ports;
using Kr.Common.Exceptions;

namespace Kr.Carevo.UMR.Application.Feature.User.Query.Details;

public class UserDetailsQueryHandler(IUserRepository repository) : IRequestHandler<UserDetailsQuery, IEnumerable<UserResponseDto>>
{
    public async Task<IEnumerable<UserResponseDto>> Handle(UserDetailsQuery request, CancellationToken cancellationToken)
    {
         switch (request.FilterMode)
        {
            case LookupMode.ById:           
                return await repository.GetDetailsByIdAsync(request.Id!.Value, cancellationToken);
            case LookupMode.ByEmail:
                return await repository.GetDetailsByEmailAsync(request.Email!, cancellationToken);
            default:
                throw new DomainValidationException($"User Lookup failed.", failures:
                        [
                            new("FilterMode", $"Invalid search criteria.")
                    ]);               
        }
    }
}