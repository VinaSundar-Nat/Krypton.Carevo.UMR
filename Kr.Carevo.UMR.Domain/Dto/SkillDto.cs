namespace Kr.Carevo.UMR.Domain.Dto;

public class SkillDto
{
    public int? Id { get; init; }

    public required string Code { get; set; }

    public required string Description { get; set; }

    public required DateTime EffectiveDate { get; set; }

    public int? UserId { get; init; }
}
