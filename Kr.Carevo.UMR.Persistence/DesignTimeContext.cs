using Kr.Common.Infrastructure.Datastore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Microsoft.Extensions.Options;

namespace Kr.Carevo.UMR.Persistence;

public class DesignTimeContext : IDesignTimeDbContextFactory<CarevoDbContext>
{
        public DesignTimeContext()
		{
		}

        public CarevoDbContext CreateDbContext(string[] args)
        {
            DbSettings dbSettings = GetConfiguration();

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(dbSettings.ConnectionString);
            dataSourceBuilder.UseNetTopologySuite();
            var dataSource = dataSourceBuilder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<CarevoDbContext>();
            optionsBuilder.UseNpgsql(dbSettings.ConnectionString, o => o.UseNetTopologySuite());

            var options = Options.Create(dbSettings);
            return new CarevoDbContext(optionsBuilder.Options, options);
        }

        public DbSettings GetConfiguration()
        {
            var path = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
               .SetBasePath(path)
               .AddJsonFile($"Configuration//Components.json")
               .AddJsonFile("appsettings.json", optional: true)
               .AddEnvironmentVariables()
               .Build();

            DbSettings dbSettings = new();
            builder.GetSection("DataStore:Carevo").Bind(dbSettings);

            return dbSettings;
        }
}