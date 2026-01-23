using Kr.Carevo.UMR.Domain.Ports;
using Kr.Carevo.UMR.Persistence.Aggregate;
using Kr.Common.Infrastructure.Datastore;
using Kr.Common.Infrastructure.Datastore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Kr.Carevo.UMR.Persistence;

public static class Startup
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
         services.Configure<DbSettings>(configuration.GetSection("DataStore:Carevo"));
         services.DbNpgContextPoolSettings<CarevoDbContext>(configuration, "DataStore:Carevo");        
         services.AddScoped<IUserRepository, UserRepository>();
         services.AddScoped<IEmploymentRepository, EmploymentRepository>();
         services.AddScoped<IApplicationRepository, ApplicationRepository>();
    }

    public static void NpgContextPoolSettings(IServiceCollection services, IConfiguration configuration, string source) 
    {
        DbSettings dbSettings = new();
        configuration.GetSection(source).Bind(dbSettings);
       
        if (dbSettings == null || !dbSettings.IsValid)
        {
            ArgumentNullException.ThrowIfNull(dbSettings, "Error :DbSettings configuration is invalid.");
        }

        NpgsqlDataSourceBuilder npgsqlDataSourceBuilder = new(dbSettings.ConnectionString);
        npgsqlDataSourceBuilder.UseNetTopologySuite();
        npgsqlDataSourceBuilder.Build();
        services.AddDbContextPool<CarevoDbContext>(delegate (IServiceProvider serviceProvider, DbContextOptionsBuilder options)
        {
            options.UseNpgsql(dbSettings.ConnectionString, delegate (NpgsqlDbContextOptionsBuilder o)
            {
                o.UseNetTopologySuite();
            });
            options.EnableSensitiveDataLogging();
        });
    }
}
