using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kr.Carevo.UMR.Persistence.Configuration;

public sealed class ProjectSkillConfiguration : IEntityTypeConfiguration<ProjectSkill>
{
    public void Configure(EntityTypeBuilder<ProjectSkill> builder)
    {
        builder.ToTable("project_skills", "carevo");
        
        builder.HasKey(ps => new { ps.ProjectId, ps.SkillId }).HasName("pk_project_skills");

        builder.Property(ps => ps.ProjectId).HasColumnName("project_id").HasColumnType("integer").IsRequired();
        builder.Property(ps => ps.SkillId).HasColumnName("skill_id").HasColumnType("integer").IsRequired();

        // Foreign keys
        builder.HasOne(ps => ps.Project)
            .WithMany(p => p.RequiredSkills)
            .HasForeignKey(ps => ps.ProjectId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_project_skills_projects");

        builder.HasOne(ps => ps.Skill)
            .WithMany(s => s.ProjectSkills)
            .HasForeignKey(ps => ps.SkillId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_project_skills_skills");
    }
}
