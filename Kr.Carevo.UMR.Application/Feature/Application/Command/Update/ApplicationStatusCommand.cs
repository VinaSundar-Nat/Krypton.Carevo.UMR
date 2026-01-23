
namespace Kr.Carevo.UMR.Application.Feature.Application.Command.Update;

public sealed class ApplicationStatusCommand : IRequest<bool>
{
    public required int UserId { get; init; }
    public required int ApplicationId { get; init; }
    public required ApplicationStatusDto StatusChange { get; set; }
}