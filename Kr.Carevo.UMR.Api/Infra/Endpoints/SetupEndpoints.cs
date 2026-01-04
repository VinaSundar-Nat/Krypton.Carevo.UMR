namespace Kr.Carevo.UMR.Api.Infra.Endpoints;

public static partial class ApiEndpoints
{
    public static void RegisterEndpoints(this IEndpointRouteBuilder app)
    {
        var versionBuilder = app.NewVersionedApi();
        UserRegistrationEndpoints(app, versionBuilder);
        UserEmploymentEndpoints(app, versionBuilder);  
    }
}