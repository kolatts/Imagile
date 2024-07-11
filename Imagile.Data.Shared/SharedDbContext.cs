using Imagile.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

public class SharedDbContext(DbContextOptions<SharedDbContext> options) : DbContext(options)
{
    public DbSet<DatabaseShard> DatabaseShards => Set<DatabaseShard>();
    public DbSet<CompanyConnection> CompanyConnections => Set<CompanyConnection>();
    public DbSet<LoginAccount> LoginAccounts => Set<LoginAccount>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanyConnection).Assembly);
        modelBuilder.SetDecimalDefaultPrecisionAndScale(18,5);
        modelBuilder.SetDateTimeOffsetDefaultPrecision(0);
    }
}

