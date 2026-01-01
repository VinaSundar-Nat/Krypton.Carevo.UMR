

using Kr.Carevo.UMR.Domain.Dto;
using Kr.Carevo.UMR.Domain.Ports;
using Kr.Common.Mediator;

namespace Kr.Carevo.UMR.Application.Feature.User.Command.Skills;

public class UserSkillsHandler(IUserRepository repository) : IRequestHandler<UserSkillsCommand, IEnumerable<SkillDto>>
{
    public async Task<IEnumerable<SkillDto>> Handle(UserSkillsCommand request, CancellationToken cancellationToken)
    {
        return await repository.UpdateSkillsAsync(request.UserId, request.Skills, cancellationToken);
    }
}