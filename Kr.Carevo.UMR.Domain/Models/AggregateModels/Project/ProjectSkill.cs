namespace Kr.Carevo.UMR.Domain.Models.AggregateModels;

public class ProjectSkill
{
    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    public int SkillId { get; set; }
    public Skill? Skill { get; set; }
}
