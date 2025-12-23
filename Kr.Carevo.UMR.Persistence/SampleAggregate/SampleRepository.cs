using System;
using Kr.Carevo.UMR.Persistence.SampleAggregate.Entity;
using Kr.Common.Infrastructure.Datastore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kr.Carevo.UMR.Persistence.SampleAggregate;

public interface ISampleRepository
{
    Task<Sample> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IList<Sample>> GetAllAsync(CancellationToken cancellationToken = default);
}

public class SampleRepository(ILogger<SampleRepository> logger, DbContext context) 
    : BaseRepository<Sample>(logger, context), ISampleRepository
{
    public Task<IList<Sample>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Sample> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
