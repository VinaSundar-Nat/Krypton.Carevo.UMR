using System;
using Kr.Carevo.UMR.Domain.Dto;
using Kr.Carevo.UMR.Domain.Ports;

namespace Kr.Carevo.UMR.Application.Feature.Sample;

public class SampleFeature(ISampleService SampleService) : ISampleFeature
{
    public async Task<SampleDto> Samples(string id)
    {
        return await SampleService.Get(id);
    }
}
