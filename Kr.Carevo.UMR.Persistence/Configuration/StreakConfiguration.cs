using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlExt = Microsoft.EntityFrameworkCore.NpgsqlPropertyBuilderExtensions;

namespace Kr.Carevo.UMR.Persistence.Configuration;

public sealed class StreakConfiguration : IEntityTypeConfiguration<Streak>
{
    public void Configure(EntityTypeBuilder<Streak> builder)
    {
        builder.ToTable("activity_streaks", "carevo");
        NpgsqlExt.UseHiLo(
            builder.Property(a => a.Id).HasColumnName("Id").HasColumnType("integer"),
            "streakseq", "carevo")
            .ValueGeneratedOnAdd();

        builder.HasKey(a => a.Id).HasName("pk_activity_streaks_Id");
        builder.Property(a => a.CreatedAt).HasColumnType("timestamptz").HasColumnName("CreatedAt")
            .ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

        builder.Property(s => s.ActivityDate).HasColumnType("date").IsRequired();
        builder.Property(s => s.ApplicationCount).HasColumnType("integer").IsRequired();
        builder.Property(s => s.ConsecutiveDayCount).HasColumnType("integer").IsRequired(false);

        // Foreign key to User
        builder.HasOne(s => s.User)
            .WithMany(u => u.ActivityStreaks)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_activity_streaks_users");

        // Create composite index on UserId and ActivityDate for query optimization
        builder.HasIndex(s => new { s.UserId, s.ActivityDate })
            .IsUnique()
            .HasDatabaseName("idx_activity_streaks_user_date");

        // Create index for consecutive streak queries
        builder.HasIndex(s => new { s.UserId, s.ConsecutiveDayCount })
            .HasDatabaseName("idx_activity_streaks_user_consecutive");
    }
}
