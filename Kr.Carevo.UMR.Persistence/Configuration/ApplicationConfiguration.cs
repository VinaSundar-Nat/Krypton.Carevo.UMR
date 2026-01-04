using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlExt = Microsoft.EntityFrameworkCore.NpgsqlPropertyBuilderExtensions;

namespace Kr.Carevo.UMR.Persistence.Configuration;

public sealed class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("applications", "carevo");
        NpgsqlExt.UseHiLo(
            builder.Property(a => a.Id).HasColumnName("Id").HasColumnType("integer"),
            "applicationseq", "carevo")
            .ValueGeneratedOnAdd();

        builder.HasKey(a => a.Id).HasName("pk_applications_Id");
        builder.Property(a => a.VersionStamp).IsRowVersion();
        builder.Property(a => a.CreatedAt).HasColumnType("timestamptz").HasColumnName("CreatedAt")
            .ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(a => a.CreatedBy).HasColumnType("varchar(500)").HasColumnName("CreatedBy")
            .IsRequired(false);

        builder.Property(a => a.JobId).HasColumnType("integer").IsRequired();
        builder.Property(a => a.Status).HasColumnType("varchar(50)").IsRequired();
        builder.Property(a => a.AppliedDate).HasColumnType("timestamptz").IsRequired();
        builder.Property(a => a.Notes).HasColumnType("varchar").IsRequired(false);

        // Foreign key to User
        builder.HasOne(a => a.User)
            .WithMany(u => u.Applications)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_applications_users");

        // One-to-many with ApplicationStatusHistory
        builder.HasMany<ApplicationStatusHistory>()
            .WithOne(h => h.Application)
            .HasForeignKey(h => h.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_application_status_history_applications");
    }
}
