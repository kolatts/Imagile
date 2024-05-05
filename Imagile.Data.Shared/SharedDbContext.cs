using Imagile.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

public class SharedDbContext : DbContext
{
    public SharedDbContext()
    {
        
    }

    public SharedDbContext(DbContextOptions<SharedDbContext> options) : base(options)
    {
        
    }

    public DbSet<DatabaseShard> DatabaseShards { get; set; }
    public DbSet<CompanyConnection> CompanyConnections { get; set; }
    public DbSet<LoginAccount> LoginAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanyConnection).Assembly);
        modelBuilder.SetDecimalDefaultPrecisionAndScale(18,5);
        modelBuilder.SetDateTimeOffsetDefaultPrecision(0);
    }
}