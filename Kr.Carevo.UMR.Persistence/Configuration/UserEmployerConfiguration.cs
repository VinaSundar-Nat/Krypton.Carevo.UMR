using Kr.Carevo.UMR.Domain.Models.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlExt = Microsoft.EntityFrameworkCore.NpgsqlPropertyBuilderExtensions;

namespace Kr.Carevo.UMR.Persistence.Configuration;

public sealed class UserEmployerConfiguration : IEntityTypeConfiguration<UserEmployer>
{
    public void Configure(EntityTypeBuilder<UserEmployer> builder)
    {
        builder.ToTable("user_employers", "carevo");

         NpgsqlExt.UseHiLo(
            builder.Property(a => a.Id).HasColumnName("Id").HasColumnType("integer"),
         "userempseq", "carevo")
         .ValueGeneratedOnAdd();

        builder.HasKey(a => a.Id).HasName("pk_useremployers_Id");

        // Create a unique index on (UserId, EmployerId) to maintain the uniqueness constraint
        builder.HasIndex(ue => new { ue.UserId, ue.EmployerId })
            .IsUnique()
            .HasDatabaseName("ix_user_employers_userid_employerid");

        builder.Property(e => e.StartDate).HasColumnType("timestamptz").IsRequired();
        builder.Property(e => e.EndDate).HasColumnType("timestamptz").IsRequired(false);
        builder.Property(ue => ue.UserId).HasColumnName("UserId").HasColumnType("integer").IsRequired();
        builder.Property(ue => ue.EmployerId).HasColumnName("EmployerId").HasColumnType("integer").IsRequired();

        // Foreign keys
        builder.HasOne(ue => ue.User)
            .WithMany(u => u.UserEmployers)
            .HasForeignKey(ue => ue.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_user_employers_users");

        builder.HasOne(ue => ue.Employer)
            .WithMany(e => e.UserEmployers)
            .HasForeignKey(ue => ue.EmployerId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_user_employers_employers");

             // One-to-many with Projects
            builder.HasMany(e => e.Projects)
            .WithOne(p => p.UserEmployer)
            .HasForeignKey(p => p.UserEmployerId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("fk_projects_useremployers");
    }
}
