namespace Kr.Carevo.UMR.Domain.Dto;

public sealed class SkillDto
{
    public int? Id { get; set; }

    public required string Code { get; set; }

    public required string Description { get; set; }

    public  DateTime? EffectiveDate { get; set; }
}
