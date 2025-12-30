
using Kr.Common.Infrastructure.Datastore;


namespace Kr.Carevo.UMR.Domain.Models.AggregateModels;

public record Coordinates(double Latitude, double Longitude);

public sealed class Address : BaseValueObject
{
    public required string Line1 { get; set; }
    public string? Line2 { get; set; }
    public string? Suburb { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string PostCode { get; set; }
    public required string Country { get; set; }
    public Coordinates? Coordinates { get; set; } = null;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Line1;
        yield return Line2;
        yield return Suburb;
        yield return City;
        yield return State;
        yield return PostCode;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Address other)
            return false;

        return Line1 == other.Line1 &&
               Line2 == other.Line2 &&
               Suburb == other.Suburb &&
               City == other.City &&
               State == other.State &&
               PostCode == other.PostCode;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Line1, Line2, Suburb, City, State, PostCode);
    }
}