using Kr.Carevo.UMR.Api.Infra.Helpers;
using Kr.Carevo.UMR.Application.Feature.User.Command.Register;
using Kr.Carevo.UMR.Application.Feature.User.Command.Skills;
using Kr.Carevo.UMR.Application.Feature.User.Query.Details;
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

        sampleGroup.MapPost("/{userId:int}/skills",
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
                "user skills update endpoint to test the api.",
                "User Skills Update",
                "Enterprise Carevo user operations."
        ));

        sampleGroup.MapGet("/{id:int}",
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
                "user details endpoint to test the api.",
                "User Details",
                "Enterprise Carevo user operations."
        ));

    }
}