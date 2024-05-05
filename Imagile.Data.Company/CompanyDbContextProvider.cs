using Imagile.Data.Shared.Connection;
using Imagile.Data.Shared.Extensions;
using Imagile.Domain.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.Data.Company;

public interface ICompanyDbContextProvider
{
    CompanyDbContext GetDbContext(int companyId, int? personId);
    CompanyDbContext GetDbContext(int databaseShardId);
}

public class CachedCompanyDbContextProvider(IServiceProvider serviceProvider, IDatabaseConnectionStringResolver databaseConnectionStringResolver) : CompanyDbContextProvider(serviceProvider, databaseConnectionStringResolver)
{
    private Dictionary<int, ImagileCompanyDatabaseConnection> _companyDatabasesByCompanyId = new();
    public bool IsExpired => Expiration <= DateTimeOffset.Now;
    public DateTimeOffset Expiration { get; set; } = DateTimeOffset.Now.AddSeconds(-5);
    private static readonly object _lock = new();
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    private void RefreshIfExpired()
    {
        if (!IsExpired) return;
        lock (_lock)
        {
            if (IsExpired) // This double check allows waiting threads to not refresh the dictionary after done waiting.
            {
                using var scope = _serviceProvider.CreateScope();
                using var context = scope.ServiceProvider.GetRequiredService<SharedDbContext>();
                _companyDatabasesByCompanyId = context.CompanyConnections.ToModels()
                    .ToDictionary(x => x.CompanyId!.Value, x => x);
                Expiration = DateTimeOffset.Now.AddMinutes(1);
            }
        }
    }
    /// <summary>
    /// Overrides the base method to use the cached dictionary.
    /// </summary>
    /// <param name="companyId"></param>
    /// <returns></returns>
    protected override async Task<ImagileCompanyDatabaseConnection?> GetConnection(int companyId)
    {
        RefreshIfExpired();
        if (_companyDatabasesByCompanyId!.TryGetValue(companyId, out var connection))
            return connection;

        var result = await GetFromDatabase(companyId);
        if (result?.CompanyId != null && !_companyDatabasesByCompanyId.ContainsKey(companyId))
            _companyDatabasesByCompanyId.Add(result.CompanyId.Value, result);

        return result;
    }
}
public class CompanyDbContextProvider(IServiceProvider serviceProvider, IDatabaseConnectionStringResolver databaseConnectionStringResolver) : ICompanyDbContextProvider
{


    public CompanyDbContext GetDbContext(int companyId, int? personId)
    {
        var connection = GetConnection(companyId).GetAwaiter().GetResult();
        if (connection == null)
            throw new CompanyNotFoundException(companyId);
        var options = GetDbContextOptions(connection);
        return new CompanyDbContext(options, companyId, personId);
    }

    public CompanyDbContext GetDbContext(int databaseShardId)
    {
        var connection = GetByShardId(databaseShardId).GetAwaiter().GetResult();
        if (connection == null)
            throw new DatabaseShardNotFoundException(databaseShardId);
        var options = GetDbContextOptions(connection);
        return new CompanyDbContext(options, null, null);
    }

    private DbContextOptions<CompanyDbContext> GetDbContextOptions(ImagileDatabaseConnection connection)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CompanyDbContext>();
        //Additional modifications can be made globally here for SQL options.
        optionsBuilder.UseSqlServer(databaseConnectionStringResolver.Resolve(connection.DataSource, connection.InitialCatalog).ToString(), options => options.UseAzureSqlDefaults());
        return optionsBuilder.Options;
    }

    protected virtual async Task<ImagileCompanyDatabaseConnection?> GetConnection(int companyId) => await GetFromDatabase(companyId);
    protected async Task<ImagileCompanyDatabaseConnection?> GetFromDatabase(int companyId)
    {
        using var scope = serviceProvider.CreateScope();
        await using var context = scope.ServiceProvider.GetRequiredService<SharedDbContext>();
        return await context.CompanyConnections.ToModels().FirstOrDefaultAsync(x => x.CompanyId == companyId);
        //if (result?.CompanyId != null && !_companyDatabasesByCompanyId.ContainsKey(companyId))
        //{
        //    _companyDatabasesByCompanyId.Add(result.CompanyId.Value, result);
        //}
    }

    protected async Task<ImagileDatabaseConnection?> GetByShardId(int databaseShardId)
    {
        using var scope = serviceProvider.CreateScope();
        await using var context = scope.ServiceProvider.GetRequiredService<SharedDbContext>();
        return await context.DatabaseShards
            .Where(x => x.DatabaseShardId == databaseShardId)
            .Select(x => new ImagileDatabaseConnection() { DataSource = x.DataSource, InitialCatalog = x.InitialCatalog })
            .FirstOrDefaultAsync();
    }
}