
namespace Kr.Carevo.UMR.Domain.Dto;

public enum ResponseStatus
{
    Created,
    Failed,
    Skipped,
}

public readonly struct ResponseItemDto
{
    public required ResponseStatus Status { get; init; }
    public required int Id { get; init; }
    public required IEnumerable<KeyValuePair<string, object?>> KeyIdentifiers { get; init; }

    public static ResponseItemDto Create(int id, IEnumerable<KeyValuePair<string, object?>> keyIdentifiers, 
            ResponseStatus status = ResponseStatus.Created)
    {
        return new ResponseItemDto
        {
            Id = id,
            Status = status,
            KeyIdentifiers = keyIdentifiers,
        };
    }
    
}

public sealed class ResponseDto
{
    public string Uri { get; set; } = string.Empty!;
    public IList<ResponseItemDto> Responses { get; set; } = [];   
}
