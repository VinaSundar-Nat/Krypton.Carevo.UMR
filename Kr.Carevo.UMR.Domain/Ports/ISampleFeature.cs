using System;
using Kr.Carevo.UMR.Domain.Dto;

namespace Kr.Carevo.UMR.Domain.Ports;

public interface ISampleFeature
{
    Task<SampleDto> Samples(string id);
}
