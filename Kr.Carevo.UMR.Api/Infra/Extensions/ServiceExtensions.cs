
using Kr.Common.Infrastructure.Http;

namespace Kr.Carevo.UMR.Api.Infra;

public static class ServiceExtensions
{
    public static void Register(this IServiceCollection services, IConfiguration configuration ){       
        RegisterExceptions(services);
        RegisterPolicies(services);     
    }

    public static void RegisterPolicies(IServiceCollection services){
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader());
        });
    }

    private static void RegisterExceptions(IServiceCollection services){ 
        services.AddProblemDetails(options => {
          options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions.Add("trace-id",
                         context.HttpContext.TraceIdentifier);
                context.ProblemDetails.Extensions.Add("path", 
                        $"{context.HttpContext.Request.Path} : {context.HttpContext.Request.Method}");
            };           
        });      
        services.AddExceptionHandler<BadRequestExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
    }
}
