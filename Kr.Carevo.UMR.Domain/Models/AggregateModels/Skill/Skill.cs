using Kr.Carevo.UMR.Domain.Dto;
using Kr.Common.Infrastructure.Datastore;
using Kr.Common.Infrastructure.Datastore.Interface;

namespace Kr.Carevo.UMR.Domain.Models.AggregateModels;

public sealed class Skill : BaseEntity<Skill>, IAggregateRoot
{
    public required string Code { get; set; }
    public required string Description { get; set; }
    public required DateTime EffectiveDate { get; set; }
    
    // Many-to-many relationships via join entities
    public ICollection<UserSkill> UserSkills { get; private set; } = [];
    public ICollection<ProjectSkill> ProjectSkills { get; private set; } = [];
    
    // Skip navigations for cleaner domain model
    public ICollection<User> Users { get; private set; } = [];
    public ICollection<Project> Projects { get; private set; } = [];    

    public void UpdateUserSkill(SkillDto skillDto)
    {
        if (string.IsNullOrWhiteSpace(skillDto.Code))
            throw new ArgumentException("Code is required.", nameof(skillDto.Code));

        if (string.IsNullOrWhiteSpace(skillDto.Description))
            throw new ArgumentException("Description is required.", nameof(skillDto.Description));

        if (skillDto.EffectiveDate == default)
            throw new ArgumentException("EffectiveDate must be a valid date.", nameof(skillDto.EffectiveDate));

        Code = skillDto.Code;
        Description = skillDto.Description;
        EffectiveDate = skillDto.EffectiveDate ?? DateTime.UtcNow;
    }
}
