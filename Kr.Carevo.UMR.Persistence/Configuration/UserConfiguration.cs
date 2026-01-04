using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlTypes;
using NpgsqlExt = Microsoft.EntityFrameworkCore.NpgsqlPropertyBuilderExtensions;

namespace Kr.Carevo.UMR.Persistence.Configuration;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", "carevo");
        NpgsqlExt.UseHiLo(
            builder.Property(a => a.Id).HasColumnName("Id").HasColumnType("integer"),
         "userseq", "carevo")
         .ValueGeneratedOnAdd();

        builder.HasKey(a => a.Id).HasName("pk_users_Id");
        builder.Property(a => a.VersionStamp).IsRowVersion();
        builder.Property(a => a.CreatedAt).HasColumnType("timestamptz").HasColumnName("CreatedAt")
            .ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(a => a.CreatedBy).HasColumnType("varchar(500)").HasColumnName("CreatedBy")
            .IsRequired(false);

        builder.Property(u => u.FirstName).HasColumnType("varchar(200)").IsRequired();
        builder.Property(u => u.LastName).HasColumnType("varchar(200)").IsRequired();
        builder.Property(u => u.Dob).HasColumnType("timestamptz").IsRequired();
        builder.Property(u => u.Status).HasColumnType("smallint").HasConversion<int>().IsRequired();

        // Owned entity: ResidentialAddress
        builder.OwnsOne(a => a.ResidentialAddress, e =>
        {
            e.Property(a => a.Line1).IsRequired().HasColumnName("Address_Line1").HasColumnType("varchar").IsRequired();
            e.Property(a => a.Line2).HasColumnName("Address_Line2").HasColumnType("varchar");
            e.Property(a => a.Suburb).HasColumnName("Address_Suburb").HasColumnType("varchar(200)");
            e.Property(a => a.City).IsRequired().HasColumnName("Address_City").HasColumnType("varchar(150)").IsRequired();
            e.Property(a => a.State).IsRequired().HasColumnName("Address_State").HasColumnType("varchar(200)").IsRequired();
            e.Property(a => a.PostCode).IsRequired().HasColumnName("Address_PostCode").HasColumnType("varchar(20)").IsRequired();
            e.Property(a => a.Country).IsRequired().HasColumnName("Address_Country").HasColumnType("varchar(100)").IsRequired();
            e.Property(a => a.Coordinates)
                .HasColumnName("Address_Coordinates").HasColumnType("point").HasConversion(
                    v => v == null ? new NpgsqlPoint(0, 0) : new NpgsqlPoint(v.Longitude, v.Latitude), 
                    v => new Coordinates(v.Y, v.X));
        });

        // Owned collection: Contacts (one-to-many, separate table)
        builder.OwnsMany(a => a.Contacts, e =>
        {
            e.ToTable("user_contacts", "carevo");
            e.WithOwner()
                .HasForeignKey("UserId")
                .HasConstraintName("fk_user_contacts_users");

            NpgsqlExt.UseHiLo(
                e.Property(c => c.Id).HasColumnName("Id").HasColumnType("integer"),
                "contactseq", "carevo")
                .ValueGeneratedOnAdd();

            e.HasKey("Id").HasName("pk_user_contacts_id");
            e.Property(c => c.Type).HasColumnName("Type").HasColumnType("varchar(50)").IsRequired();
            e.Property(c => c.Value).HasColumnName("Value").HasColumnType("varchar(500)").IsRequired();
            e.Property(a => a.CreatedAt).HasColumnType("timestamptz").HasColumnName("CreatedAt")
            .ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            e.Property(a => a.CreatedBy).HasColumnType("varchar(500)").HasColumnName("CreatedBy")
                .IsRequired(false);
        });

        // One-to-many: Individual Projects
        builder.HasMany(a => a.IndividualProjects)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_projects_users_individual");

        // One-to-many: Applications
        builder.HasMany(a => a.Applications)
            .WithOne(app => app.User)
            .HasForeignKey(app => app.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_applications_users");

        // One-to-many: Activity Streaks
        builder.HasMany(a => a.ActivityStreaks)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_activity_streaks_users");
    }
}
