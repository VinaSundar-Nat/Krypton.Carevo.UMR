using Kr.Carevo.UMR.Api.Infra.Helpers;
using Kr.Carevo.UMR.Domain.Dto;
using Kr.Carevo.UMR.Domain.Ports;
using Kr.Common.Infrastructure.Http.Models;
using Microsoft.AspNetCore.Mvc;


namespace Kr.Carevo.UMR.Api.Infra.Endpoints;

public static partial class ApiEndpoints
{
    public static void UserRegistrationEndpoints(this WebApplication app)
    {
        var sampleGroup = app.MapGroup("/api/user/register/v1");
        sampleGroup.MapPost("/", async ([FromBody] UserDto user,[AsParameters] ApiHeaders request, 
                HttpContext context,
                [FromServices] IUserRegistrationFeature userRegistrationFeature,
                CancellationToken token = default) =>
        {
            var registedUser = await userRegistrationFeature.Register(user);
            return Results.Ok(new ApiSuccessResponse<int?> { StatusCode = 200, Url = context.Request.Path ,Data = registedUser?.Id });
        }).WithOpenApi(operation =>
            operation.GenerateOpenApiDoc(
                "v1 user registration.",
                "user registration endpoint to test the api.",
                "User Registration",
                "Enterprise Carevo user operations."
        ))
        .Produces<ApiSuccessResponse<int?>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
