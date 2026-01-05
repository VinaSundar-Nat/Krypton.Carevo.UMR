
using Kr.Carevo.UMR.Application.Feature.User.Command.Employment;
using Kr.Carevo.UMR.Application.Feature.User.Query.Employment;

namespace Kr.Carevo.UMR.Api.Infra.Endpoints;

 public static partial class ApiEndpoints
{
    public static void UserEmploymentEndpoints(IEndpointRouteBuilder app ,IVersionedEndpointRouteBuilder umrApp)
    {
        var employmentGroup = umrApp.MapGroup("/api/user/v1").HasApiVersion( 1.0 );
        employmentGroup.MapPost("/{userId:int}/employment",
        [ProducesResponseType<ApiSuccessResponse<IEnumerable<EmploymentDto>>>(StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json")]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
         async ([FromRoute] int userId, [FromBody] IEnumerable<EmploymentDto> employments,
                HttpContext context,
                [FromServices] IMediate mediate,
                CancellationToken token = default) =>
        {
            var response = await mediate.Send(new UserEmploymentCommand { UserId = userId, Employments = employments }, token);
            response.Uri = $"/api/user/v1/{userId}/employment";
            return TypedResults.Ok(response);
        }).WithOpenApi(operation =>
            operation.GenerateOpenApiDoc(
                "v1 user employment update.",
                "user employment update endpoint for UMR.",
                "User Employment Update",
                "Enterprise Carevo user operations."
        ));

        employmentGroup.MapGet("/{id:int}/employments",
        [ProducesResponseType<ApiSuccessResponse<IEnumerable<EmploymentResponseDto>>>(StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json")]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
         async ([FromRoute] int id,
                HttpContext context,
                [FromServices] IMediate mediate,
                CancellationToken token = default) =>
        {
            var userDetails = await mediate.Send(new UserEmploymentQuery(UserId: id), token);
            return TypedResults.Ok(userDetails);
        }).WithOpenApi(operation =>
            operation.GenerateOpenApiDoc(
                "v1 user employment details.",
                "user employment details endpoint for UMR.",
                "User Employment Details",
                "Enterprise Carevo user operations."
        ));
    }
}
            