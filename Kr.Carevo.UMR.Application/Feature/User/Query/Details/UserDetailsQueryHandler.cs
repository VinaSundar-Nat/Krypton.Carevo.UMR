

using Kr.Carevo.UMR.Domain.Dto;
using Kr.Carevo.UMR.Domain.Ports;
using Kr.Common.Exceptions;
using Kr.Common.Mediator;

namespace Kr.Carevo.UMR.Application.Feature.User.Query.Details;

public class UserDetailsQueryHandler(IUserRepository repository) : IRequestHandler<UserDetailsQuery, IEnumerable<UserResponseDto>>
{
    public async Task<IEnumerable<UserResponseDto>> Handle(UserDetailsQuery request, CancellationToken cancellationToken)
    {
         switch (request.FilterMode)
        {
            case LookupMode.ById:
                if (request.Id is null || request.Id <= 0)
                    throw new DomainValidationException($"User Lookup failed.", failures:
                        [
                            new("UserId", $"Invalid user identifier.")
                ]);

                return await repository.GetDetailsByIdAsync(request.Id.Value, cancellationToken);
            case LookupMode.ByEmail:
                if (string.IsNullOrWhiteSpace(request.Email))
                    throw new DomainValidationException($"User Lookup failed.", failures:
                        [
                            new("Email", $"Invalid user email.")
                    ]);
                return await repository.GetDetailsByEmailAsync(request.Email!, cancellationToken);
            default:
                throw new DomainValidationException($"User Lookup failed.", failures:
                        [
                            new("FilterMode", $"Invalid search criteria.")
                    ]);               
        }
    }
}