using Kr.Common.Infrastructure.Datastore;
using Kr.Common.Infrastructure.Datastore.Interface;

namespace Kr.Carevo.UMR.Domain.Models.AggregateModels;

public sealed class Project : BaseEntity<Project>, IAggregateRoot
{
    public required string Title { get;  set; }
    public required string Description { get; set; }
    public int? UserEmployerId { get; set; }
    public UserEmployer? UserEmployer { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }   
    public ICollection<ProjectSkill> RequiredSkills { get; private set; } = [];
    public ICollection<Skill> Skills { get; set; } =[];
   
    public void UpdateProject(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));

        Title = title;
        Description = description;
    }

   

    public void AddSkill(Skill skill)
    {
        ArgumentNullException.ThrowIfNull(skill, nameof(skill));

        if (this.RequiredSkills.Any(ps => ps.SkillId == skill.Id))
            throw new InvalidOperationException($"Skill '{skill.Code}' (ID: {skill.Id}) is already associated with this project.");

        var projectSkill = new ProjectSkill
        {
            ProjectId = this.Id,
            Project = this,
            SkillId = skill.Id,
            Skill = skill
        };

        this.RequiredSkills.Add(projectSkill);
    }

    public void RemoveSkill(int skillId)
    {
        if (skillId <= 0)
            throw new ArgumentException("Skill ID must be valid.", nameof(skillId));

        var projectSkill = this.RequiredSkills.FirstOrDefault(ps => ps.SkillId == skillId);

        if (projectSkill == null)
            throw new InvalidOperationException($"Skill with ID '{skillId}' is not associated with this project.");

        this.RequiredSkills.Remove(projectSkill);
    }

    public IEnumerable<int> GetSkillIds() => this.RequiredSkills.Select(ps => ps.SkillId);

    public IEnumerable<Skill> GetSkills() => this.RequiredSkills.Where(ps => ps.Skill != null).Select(ps => ps.Skill!);
}
