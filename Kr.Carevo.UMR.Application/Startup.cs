using Kr.Carevo.UMR.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Kr.Common.Mediator;
using Kr.Carevo.UMR.Application.Mapper;

namespace Kr.Carevo.UMR.Application;

public static class Startup
{
     public static void RegisterFeatures(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigurePersistence(configuration);
        services.AddHttpContextAccessor();
        services.AddMediator([typeof(Startup).Assembly]);
        services.AddAutoMapper(cfg => 
        {
            cfg.AddProfile<UserProfile>();
        });
    }

}
