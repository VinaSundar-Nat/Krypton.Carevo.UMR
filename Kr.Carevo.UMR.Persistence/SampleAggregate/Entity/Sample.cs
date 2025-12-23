using Kr.Carevo.UMR.Persistence.SampleAggregate.ValueObject;
using Kr.Common.Infrastructure.Datastore;

namespace Kr.Carevo.UMR.Persistence.SampleAggregate.Entity;

public sealed class Sample : BaseEntity<Sample>
{
    public string? Name { get; set; }
    public Price? Price { get; set; }    
}
