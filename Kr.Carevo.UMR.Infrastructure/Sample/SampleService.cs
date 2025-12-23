using System;
using Kr.Carevo.UMR.Domain.Common;
using Kr.Carevo.UMR.Domain.Dto;
using Kr.Carevo.UMR.Domain.Ports;
using Microsoft.Extensions.Options;

namespace Kr.Carevo.UMR.Infrastructure.Sample;

public class SampleService(IOptions<ServiceConfiguration> ServiceOptions) : ISampleService
{
    private readonly ServiceConfiguration _serviceConfiguration = ServiceOptions.Value;
    
    public async Task<SampleDto> Get(string id)
    {
        throw new NotImplementedException();
    }
}
