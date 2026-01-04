using Kr.Carevo.UMR.Application.Feature.User.Command.Register;
using Kr.Carevo.UMR.Application.Feature.User.Command.Skills;
using Kr.Carevo.UMR.Application.Feature.User.Query.Details;

namespace Kr.Carevo.UMR.Api.Infra.Endpoints;

public static partial class ApiEndpoints
{
    private static void UserRegistrationEndpoints(IEndpointRouteBuilder app ,IVersionedEndpointRouteBuilder umrApp)
    {
        var userApi = app.NewVersionedApi();
        var userGroup = umrApp.MapGroup("/api/user/v1").HasApiVersion( 1.0 );
        userGroup.MapPost("/register",
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
                "user registration endpoint for UMR.",  
                "User Registration",
                "Enterprise Carevo user operations."
        ));

        userGroup.MapPost("/{userId:int}/skills",
        [ProducesResponseType<ApiSuccessResponse<IEnumerable<SkillDto>>>(StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json")]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
         async ([FromRoute] int userId, [FromBody] IEnumerable<SkillDto> skills,
                HttpContext context,
                [FromServices] IMediate mediate,
                CancellationToken token = default) =>
        {
            var updatedSkills = await mediate.Send(new UserSkillsCommand { UserId = userId, Skills = skills }, token);
            return TypedResults.Ok(updatedSkills);
        }).WithOpenApi(operation =>
            operation.GenerateOpenApiDoc(
                "v1 user skills update.",
                "user skills update endpoint for UMR.",
                "User Skills Update",
                "Enterprise Carevo user operations."
        ));

        userGroup.MapGet("/{id:int}",
        [ProducesResponseType<ApiSuccessResponse<IEnumerable<SkillDto>>>(StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json")]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
         async ([FromRoute] int id,
                HttpContext context,
                [FromServices] IMediate mediate,
                CancellationToken token = default) =>
        {
            var userDetails = await mediate.Send(new UserDetailsQuery(LookupMode.ById, Id: id), token);
            return TypedResults.Ok(userDetails);
        }).WithOpenApi(operation =>
            operation.GenerateOpenApiDoc(
                "v1 user details.",
                "user details endpoint for UMR.",
                "User Details",
                "Enterprise Carevo user operations."
        ));

    }
}