using Kr.Carevo.UMR.Persistence.SampleAggregate.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlExt = Microsoft.EntityFrameworkCore.NpgsqlPropertyBuilderExtensions;
//using SqlServerExt = Microsoft.EntityFrameworkCore.SqlServerPropertyBuilderExtensions;


namespace Kr.Carevo.UMR.Persistence.Configuration;

public sealed class SampleConfiguration : IEntityTypeConfiguration<Sample>
{
    public void Configure(EntityTypeBuilder<Sample> builder)
    {
        builder.ToTable("Samples");
        NpgsqlExt.UseHiLo(
            builder.Property(a => a.Id).HasColumnName("Id").HasColumnType("integer"),
         "sampleseq", "kr")
         .ValueGeneratedOnAdd();

        builder.HasKey(a => a.Id).HasName("pk_Samples_Id");
        builder.Property(a => a.VersionStamp).IsRowVersion();
        builder.Property(a => a.CreatedAt).HasColumnType("timestamp").HasColumnName("CreatedAt")
            .ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(a => a.CreatedBy).HasColumnType("varchar(500)").HasColumnName("CreatedBy")
            .IsRequired(false);

        builder.OwnsOne(a => a.Price, e =>
        {
            e.Property(a => a.Currency).IsRequired().HasColumnName("CurrencyCode")
                .HasColumnType("varchar(3)").HasMaxLength(3);
            e.Property(a => a.Amount).IsRequired().HasColumnName("Amount");
        });
    }
}
