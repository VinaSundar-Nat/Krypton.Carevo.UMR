using Kr.Carevo.UMR.Domain.Models.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlExt = Microsoft.EntityFrameworkCore.NpgsqlPropertyBuilderExtensions;

namespace Kr.Carevo.UMR.Persistence.Configuration;

public sealed class ApplicationStatusHistoryConfiguration : IEntityTypeConfiguration<ApplicationStatusHistory>
{
    public void Configure(EntityTypeBuilder<ApplicationStatusHistory> builder)
    {
        builder.ToTable("application_status_history", "carevo");
        NpgsqlExt.UseHiLo(
            builder.Property(a => a.Id).HasColumnName("Id").HasColumnType("integer"),
            "applicationhistoryseq", "carevo")
            .ValueGeneratedOnAdd();

        builder.HasKey(a => a.Id).HasName("pk_application_status_history_Id");
        builder.Property(a => a.CreatedAt).HasColumnType("timestamp").HasColumnName("CreatedAt")
            .ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

        builder.Property(h => h.Status).HasColumnType("varchar(50)").IsRequired();
        builder.Property(h => h.PreviousStatus).HasColumnType("varchar(50)").IsRequired(false);
        builder.Property(h => h.StatusChangedDate).HasColumnType("timestamp").IsRequired();
        builder.Property(h => h.Notes).HasColumnType("varchar").IsRequired(false);
        builder.Property(h => h.Reason).HasColumnType("varchar(500)").IsRequired(false);
        builder.Property(h => h.ChangedBy).HasColumnType("varchar(500)").IsRequired(false);

        // Foreign key to Application
        builder.HasOne(h => h.Application)
            .WithMany()
            .HasForeignKey(h => h.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_application_status_history_applications");

        // Create index on ApplicationId and StatusChangedDate for query optimization
        builder.HasIndex(h => new { h.ApplicationId, h.StatusChangedDate }).HasDatabaseName("idx_app_history_appid_date");
    }
}
