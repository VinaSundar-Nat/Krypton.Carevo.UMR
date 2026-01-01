namespace Kr.Carevo.UMR.Domain.Dto;

public sealed class ProjectDto
{
    public int? Id { get; init; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public int? EmploymentId { get; init; }

    public int? UserId { get; init; }

    public IEnumerable<SkillDto> Skills { get; set; } = [];
}
