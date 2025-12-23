using System;
using Kr.Carevo.UMR.Domain.Dto;

namespace Kr.Carevo.UMR.Domain.Ports;

public interface ISampleService
{
    Task<SampleDto> Get(string id);
}
