using Kr.Carevo.UMR.Domain.Models.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlExt = Microsoft.EntityFrameworkCore.NpgsqlPropertyBuilderExtensions;

namespace Kr.Carevo.UMR.Persistence.Configuration;

public sealed class EmploymentConfiguration : IEntityTypeConfiguration<Employment>
{
    public void Configure(EntityTypeBuilder<Employment> builder)
    {
        builder.ToTable("employments", "carevo");
        NpgsqlExt.UseHiLo(
            builder.Property(a => a.Id).HasColumnName("Id").HasColumnType("integer"),
            "employmentseq", "carevo")
            .ValueGeneratedOnAdd();

        builder.HasKey(a => a.Id).HasName("pk_employments_Id");
        builder.Property(a => a.VersionStamp).IsRowVersion();
        builder.Property(a => a.CreatedAt).HasColumnType("timestamp").HasColumnName("CreatedAt")
            .ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(a => a.CreatedBy).HasColumnType("varchar(500)").HasColumnName("CreatedBy")
            .IsRequired(false);

        builder.Property(e => e.Company).HasColumnType("varchar(500)").IsRequired();
        builder.Property(e => e.StartDate).HasColumnType("timestamp").IsRequired();
        builder.Property(e => e.EndDate).HasColumnType("timestamp").IsRequired(false);
        builder.Property(e => e.Logo).HasColumnType("varchar").IsRequired(false);
        builder.Property(e => e.Url).HasColumnType("varchar").IsRequired(false);

        // Foreign key to User
        builder.HasOne(e => e.User)
            .WithMany(u => u.Employments)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_employments_users");

        // One-to-many with Projects
        builder.HasMany(e => e.Projects)
            .WithOne(p => p.Employment)
            .HasForeignKey(p => p.EmploymentId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("fk_projects_employments");
    }
}
