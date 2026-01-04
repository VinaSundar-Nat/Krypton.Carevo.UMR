using Kr.Common.Infrastructure.Datastore;
using Kr.Common.Infrastructure.Datastore.Interface;

namespace Kr.Carevo.UMR.Domain.Models.AggregateModels;

public sealed class Project : BaseEntity<Project>, IAggregateRoot
{
    public string Title { get;  private set; } = string.Empty!;
    public string Description { get; private set; } = string.Empty!;
    public int? UserEmployerId { get; private set; }
    public UserEmployer? UserEmployer { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }   
    public ICollection<ProjectSkill> RequiredSkills { get; private set; } = [];
    public ICollection<Skill> Skills { get; set; } =[];

    public void CreateProject(string title, string description, int? userEmployerId = null, int? userId = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));

        Title = title;
        Description = description;

        if (userEmployerId.HasValue)
            UserEmployerId = userEmployerId.Value;

        if (userId.HasValue)
            UserId = userId.Value;
    }


    public void AddSkill(SkillDto skill, int? projectId = null)
    {

        this.RequiredSkills.Add(new ProjectSkill
        {
            Skill = new Skill
            {
                Code = skill.Code,
                Description = skill.Description,
                EffectiveDate = skill.EffectiveDate ?? DateTime.UtcNow
            },
            ProjectId =  projectId ?? this.Id,
        });
    }
   
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
