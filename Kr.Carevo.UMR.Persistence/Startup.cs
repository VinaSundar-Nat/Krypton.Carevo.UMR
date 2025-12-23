using Kr.Common.Infrastructure.Datastore;
using Kr.Common.Infrastructure.Datastore.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kr.Carevo.UMR.Persistence;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
         services.Configure<DbSettings>(configuration.GetSection("DataStore:Sample"));
         services.DbNpgContextPoolSettings<SampleDbContext>(configuration, Constants.SampleDataKey);
    }
}
