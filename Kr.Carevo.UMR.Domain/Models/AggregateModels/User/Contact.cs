using Kr.Common.Infrastructure.Datastore;

namespace Kr.Carevo.UMR.Domain.Models.AggregateModels;

public enum ContactType
{
    Email,
    Phone,
    Mobile
}

public sealed class Contact : BaseValueObject
{
    public int Id { get; set; }
    public required ContactType Type { get; set; }
    public required string Value { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public uint VersionStamp { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Type;
        yield return Value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Contact other)
            return false;

        return Type == other.Type &&
               Value == other.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Value);
    }

}
