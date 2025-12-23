using Kr.Carevo.UMR.Infrastructure;
using Kr.Carevo.UMR.Application.Feature.Sample;
using Kr.Carevo.UMR.Domain.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kr.Carevo.UMR.Application;

public static class Startup
{
     public static void RegisterFeatures(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterServices(configuration);
        services.AddScoped<ISampleFeature, SampleFeature>();
    }

}
