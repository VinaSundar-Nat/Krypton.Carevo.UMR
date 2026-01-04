using Kr.Carevo.UMR.Domain.Ports;
using Kr.Carevo.UMR.Persistence.Aggregate;
using Kr.Common.Infrastructure.Datastore;
using Kr.Common.Infrastructure.Datastore.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kr.Carevo.UMR.Persistence;

public static class Startup
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
         services.Configure<DbSettings>(configuration.GetSection("DataStore:Carevo"));
         services.DbNpgContextPoolSettings<CarevoDbContext>(configuration, "DataStore:Carevo");        
         services.AddScoped<IUserRepository, UserRepository>();
         services.AddScoped<IEmploymentRepository, EmploymentRepository>();
    }
}
