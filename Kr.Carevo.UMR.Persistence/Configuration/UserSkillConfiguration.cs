using Kr.Carevo.UMR.Domain.Models.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kr.Carevo.UMR.Persistence.Configuration;

public sealed class UserSkillConfiguration : IEntityTypeConfiguration<UserSkill>
{
    public void Configure(EntityTypeBuilder<UserSkill> builder)
    {
        builder.ToTable("user_skills", "carevo");
        
        builder.HasKey(us => new { us.UserId, us.SkillId }).HasName("pk_user_skills");

        builder.Property(us => us.UserId).HasColumnName("user_id").HasColumnType("integer").IsRequired();
        builder.Property(us => us.SkillId).HasColumnName("skill_id").HasColumnType("integer").IsRequired();

        // Foreign keys
        builder.HasOne(us => us.User)
            .WithMany(u => u.UserSkills)
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_user_skills_users");

        builder.HasOne(us => us.Skill)
            .WithMany(s => s.UserSkills)
            .HasForeignKey(us => us.SkillId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_user_skills_skills");
    }
}
