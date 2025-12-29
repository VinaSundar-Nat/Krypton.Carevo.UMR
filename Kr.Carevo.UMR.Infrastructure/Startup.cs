using System;
using System.Net;
using Kr.Carevo.UMR.Domain.Common;
using Kr.Carevo.UMR.Domain.Ports;
using Kr.Common.Infrastructure.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kr.Carevo.UMR.Infrastructure;

public static class Startup
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.Configure<ServiceConfiguration>(configuration.GetSection("Service:SampleService"));
    }  
}
