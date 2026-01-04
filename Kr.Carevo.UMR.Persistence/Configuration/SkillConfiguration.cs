using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlExt = Microsoft.EntityFrameworkCore.NpgsqlPropertyBuilderExtensions;

namespace Kr.Carevo.UMR.Persistence.Configuration;

public sealed class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("skills", "carevo");
        NpgsqlExt.UseHiLo(
            builder.Property(a => a.Id).HasColumnName("id").HasColumnType("integer"),
            "skillseq", "carevo")
            .ValueGeneratedOnAdd();

        builder.HasKey(a => a.Id).HasName("pk_skills_id");
        builder.Property(a => a.VersionStamp).IsRowVersion();
        builder.Property(a => a.CreatedAt).HasColumnType("timestamptz").HasColumnName("created_at")
            .ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(a => a.CreatedBy).HasColumnType("varchar(500)").HasColumnName("created_by")
            .IsRequired(false);

        builder.Property(s => s.Code).HasColumnName("code").HasColumnType("varchar(100)").IsRequired();
        builder.Property(s => s.Description).HasColumnName("description").HasColumnType("varchar").IsRequired();
        builder.Property(s => s.EffectiveDate).HasColumnName("effective_date").HasColumnType("timestamptz").IsRequired();

        // Many-to-many with User via UserSkill join entity
        builder.HasMany(s => s.Users)
            .WithMany(u => u.Skills)
            .UsingEntity<UserSkill>(
                j => j.HasOne(us => us.User).WithMany(u => u.UserSkills).HasForeignKey(us => us.UserId).HasConstraintName("fk_user_skills_users"),
                j => j.HasOne(us => us.Skill).WithMany(s => s.UserSkills).HasForeignKey(us => us.SkillId).HasConstraintName("fk_user_skills_skills"),
                j =>
                {
                    j.ToTable("user_skills", "carevo");
                    j.HasKey(us => new { us.UserId, us.SkillId }).HasName("pk_user_skills");
                });

        // Many-to-many with Project via ProjectSkill join entity
        builder.HasMany(s => s.Projects)
            .WithMany(p => p.Skills)
            .UsingEntity<ProjectSkill>(
                j => j.HasOne(ps => ps.Project).WithMany(p => p.RequiredSkills).HasForeignKey(ps => ps.ProjectId).HasConstraintName("fk_project_skills_projects"),
                j => j.HasOne(ps => ps.Skill).WithMany(s => s.ProjectSkills).HasForeignKey(ps => ps.SkillId).HasConstraintName("fk_project_skills_skills"),
                j =>
                {
                    j.ToTable("project_skills", "carevo");
                    j.HasKey(ps => new { ps.ProjectId, ps.SkillId }).HasName("pk_project_skills");
                });
    }
}
