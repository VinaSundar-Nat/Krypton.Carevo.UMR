using Kr.Carevo.UMR.Domain.Models.AggregateModels;
using Kr.Carevo.UMR.Persistence.Configuration;
using Kr.Common.Infrastructure.Datastore;
using Kr.Common.Infrastructure.Datastore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Kr.Carevo.UMR.Persistence;

public class CarevoDbContext(DbContextOptions<CarevoDbContext> options,
    IOptions<DbSettings> dbSettings) : BaseContext<CarevoDbContext>(options, dbSettings)
{
    // Aggregate root entities
    public DbSet<User> Users { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Employer> Employers { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<ApplicationStatusHistory> ApplicationStatusHistories { get; set; }
    public DbSet<Streak> ActivityStreaks { get; set; }
    public DbSet<UserSkill> UserSkills { get; set; }
    public DbSet<ProjectSkill> ProjectSkills { get; set; }
    public DbSet<UserEmployer> UserEmployers { get; set; }

    public override Task NotifyChanges()
    {
        return Task.CompletedTask;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("carevo");
 
        base.OnModelCreating(modelBuilder);

        CreateSequences(modelBuilder);

        modelBuilder.ApplyAllConfigurations(typeof(UserConfiguration));
    }

    private static void CreateSequences(ModelBuilder modelBuilder)
    {
        // User sequence
        modelBuilder.HasSequence<int>("userseq", "carevo")
            .StartsAt(1)
            .IncrementsBy(1);

        // Skill sequence
        modelBuilder.HasSequence<int>("skillseq", "carevo")
            .StartsAt(1)
            .IncrementsBy(1);

        // Project sequence
        modelBuilder.HasSequence<int>("projectseq", "carevo")
            .StartsAt(1)
            .IncrementsBy(1);

        // Employer sequence
        modelBuilder.HasSequence<int>("employerseq", "carevo")
            .StartsAt(1)
            .IncrementsBy(1);

        // user Employer sequence
        modelBuilder.HasSequence<int>("userempseq", "carevo")
            .StartsAt(1)
            .IncrementsBy(1);

        // Application sequence
        modelBuilder.HasSequence<int>("applicationseq", "carevo")
            .StartsAt(1)
            .IncrementsBy(1);

        // Application history sequence
        modelBuilder.HasSequence<int>("applicationhistoryseq", "carevo")
            .StartsAt(1)
            .IncrementsBy(1);

        // Streak sequence
        modelBuilder.HasSequence<int>("streakseq", "carevo")
            .StartsAt(1)
            .IncrementsBy(1);

        // Contact sequence (for user_contacts owned entity)
        modelBuilder.HasSequence<int>("contactseq", "carevo")
            .StartsAt(1)
            .IncrementsBy(1);
    }
}
