using Kr.Carevo.UMR.Domain.Dto;
using Kr.Common.Mediator;

namespace Kr.Carevo.UMR.Application.Feature.User.Command.Skills;

public sealed class UserSkillsCommand : IRequest<IEnumerable<SkillDto>>
{
    public required int UserId { get; set; }
    public required IEnumerable<SkillDto> Skills { get; init; }
}