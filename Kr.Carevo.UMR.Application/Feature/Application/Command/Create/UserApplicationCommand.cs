namespace Kr.Carevo.UMR.Application.Feature.Application.Command.Create;

public sealed class UserApplicationCommand : IRequest<ApplicationResponseDto>
{
    public required int UserId { get; init; }
    public required ApplicationDto Application { get; init; }
}
