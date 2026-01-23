using Kr.Carevo.UMR.Application.Feature.Application.Command.Create;
using Kr.Carevo.UMR.Application.Feature.Application.Command.Update;
using Kr.Carevo.UMR.Application.Feature.Application.Query.Fetch;

namespace Kr.Carevo.UMR.Api.Infra.Endpoints;

public static partial class ApiEndpoints
{
    public static void ApplicationEndpoints(IEndpointRouteBuilder app, IVersionedEndpointRouteBuilder umrApp)
    {
        var applicationGroup = umrApp.MapGroup("/api/application/v1").HasApiVersion(1.0);
        applicationGroup.MapPost("/register",
        [ProducesResponseType<ApiSuccessResponse<ApplicationResponseDto>>(StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json")]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
        async ([FromBody] ApplicationDto application,
                HttpContext context,
                [FromServices] IMediate mediate,
                CancellationToken token = default) =>
        {
            var response = await mediate.Send(new UserApplicationCommand { UserId = application.UserId, Application = application }, token);
            return TypedResults.Created($"/api/application/v1/{response.Id}");
        }).WithOpenApi(operation =>
            operation.GenerateOpenApiDoc(
                "v1 user application create.",
                "user application create endpoint for UMR.",
                "User Application Create",
                "Enterprise Carevo user operations."
        ));

        applicationGroup.MapPut("/{applicationId:int}/status",
        [ProducesResponseType<ApiSuccessResponse<bool>>(StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json")]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
        async ([FromRoute] int applicationId, [FromBody] ApplicationStatusDto statusUpdate,
                HttpContext context,
                //TO DO: Replace with authenticated user id
                [FromHeader(Name = "X-User-Id")] int userId,
                [FromServices] IMediate mediate,
                CancellationToken token = default) =>
        {
            var response = await mediate.Send(new ApplicationStatusCommand { UserId = userId, ApplicationId = applicationId, StatusChange = statusUpdate }, token);
            return TypedResults.NoContent();
        }).WithOpenApi(operation =>
            operation.GenerateOpenApiDoc(
                "v1 application status update.",
                "application status update endpoint for UMR.",
                "Application Status Update",
                "Enterprise Carevo user operations."));

        applicationGroup.MapGet("/{applicationId:int}",
        [ProducesResponseType<ApiSuccessResponse<ApplicationResponseDto>>(StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json")]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
        async ([FromRoute] int applicationId,
                HttpContext context,
                [FromServices] IMediate mediate,
                CancellationToken token = default) =>
        {
            var response = await mediate.Send(new ApplicationQuery(applicationId), token);
            return TypedResults.Ok(response);
        }).WithOpenApi(operation =>
            operation.GenerateOpenApiDoc(
                "v1 fetch application by id.",
                "fetch application by id endpoint for UMR.",
                "Fetch Application By Id",
                "Enterprise Carevo user operations."));                                  
    }
}