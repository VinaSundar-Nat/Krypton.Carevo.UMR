using Kr.Carevo.UMR.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Kr.Common.Infrastructure.Resolver;

namespace Kr.Carevo.UMR.Application;

public static class Startup
{
     public static void RegisterFeatures(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigurePersistence(configuration);
        services.AddMediator([typeof(Startup).Assembly]);
    }

}
