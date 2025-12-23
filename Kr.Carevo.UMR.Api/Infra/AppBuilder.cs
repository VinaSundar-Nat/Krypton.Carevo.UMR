using System.Net;
using System.Security.Authentication;
using Kr.Carevo.UMR.Application;
using Kr.Carevo.UMR.Domain.Models.Infrastructure;
using Kr.Common.Infrastructure.Logging;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Kr.Carevo.UMR.Api.Infra;

public static class AppBuilder
{
    public static void Setup(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        Configuration(builder);
        builder.RegisterLogger();
        Host(builder);
        builder.Services.Register(builder.Configuration);
        builder.Services.RegisterFeatures(builder.Configuration);     
    }

    private static void Configuration(WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
        builder.Configuration.AddJsonFile("./Configuration/Serilog.json", optional: false);
        builder.Configuration.AddJsonFile("./Configuration/Components.json", optional: false);
        builder.Configuration.AddJsonFile($"./Configuration/Components.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);
    }

    private static void Host(WebApplicationBuilder builder)
    {
        builder.WebHost.UseKestrel((WebHostBuilderContext context, KestrelServerOptions options) =>
        {
            var hostConfig = new KerstalConfiguration();
            context.Configuration.GetSection(KerstalConfiguration.HostingOptions).Bind(hostConfig);

            ArgumentNullException.ThrowIfNull(hostConfig, nameof(hostConfig));
          

            if (!hostConfig?.UseKerstal ?? false)
                return;

            options.ConfigureHttpsDefaults(httpsOptions =>
            {
                httpsOptions.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
            });

            options.Listen(IPAddress.Any, hostConfig!.Port, listenOptions =>
            {
                try
                {
                    listenOptions.Protocols = HttpProtocols.Http1;
                    listenOptions.UseHttps(hostConfig.CertPath, hostConfig.CertPassword);
                }
                catch (Exception ex)
                {
                    var loggerFactory = builder.Services.BuildServiceProvider()
                                            .GetRequiredService<ILoggerFactory>();
                    var logger = loggerFactory.CreateLogger("Application");
                    logger.LogApplicationException(ex);
                }
            });

            options.Listen(IPAddress.Any, 5002, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });

            options.Listen(IPAddress.Any, 8085, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1;
            });

            options.Limits.MaxRequestBodySize = 9703180;           
            options.Configure();
        });
    }


}
