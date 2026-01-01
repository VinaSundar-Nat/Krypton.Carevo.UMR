using Kr.Carevo.UMR.Api.Infra.Helpers;
using Kr.Carevo.UMR.Application.Feature.User.Command.Register;
using Kr.Carevo.UMR.Domain.Dto;
using Kr.Common.Infrastructure.Http.Models;
using Kr.Common.Mediator;
using Microsoft.AspNetCore.Mvc;


namespace Kr.Carevo.UMR.Api.Infra.Endpoints;

public static partial class ApiEndpoints
{
    public static void UserRegistrationEndpoints(this IEndpointRouteBuilder app)
    {
        var sampleGroup = app.MapGroup("/api/user/v1");
        sampleGroup.MapPost("/register",
        [ProducesResponseType<ApiSuccessResponse<int?>>(StatusCodes.Status201Created, "application/json")]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json")]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
         async ([FromBody] UserDto user,
                HttpContext context,
                [FromServices] IMediate mediate,
                CancellationToken token = default) =>
        {
            var registedUser = await mediate.Send(new UserRegistrationCommand{ User = user }, token);
            return TypedResults.Created($"/api/user/v1/{registedUser?.Id}");
        }).WithOpenApi(operation =>
            operation.GenerateOpenApiDoc(
                "v1 user registration.",
                "user registration endpoint to test the api.",  
                "User Registration",
                "Enterprise Carevo user operations."
        ));
    }
}
