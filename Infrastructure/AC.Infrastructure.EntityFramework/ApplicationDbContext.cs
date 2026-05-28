using AC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AC.Infrastructure.EntityFramework;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Plaintiff> Plaintiffs { get; set; }
    public DbSet<Defendant> Defendants { get; set; }
    public DbSet<Arbitrator> Arbitrators { get; set; }
    public DbSet<Case> Cases { get; set; }
    public DbSet<Claim> Claims { get; set; }
    public DbSet<Verdict> Verdicts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<SettlementProposal> SettlementProposals { get; set; }
    public DbSet<CourtRule> CourtRules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
