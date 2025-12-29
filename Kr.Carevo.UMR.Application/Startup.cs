using Kr.Carevo.UMR.Persistence;
using Kr.Carevo.UMR.Domain.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Kr.Carevo.UMR.Application.Feature.User;

namespace Kr.Carevo.UMR.Application;

public static class Startup
{
     public static void RegisterFeatures(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigurePersistence(configuration);
        services.AddScoped<IUserRegistrationFeature, UserRegistrationFeature>();
    }

}
