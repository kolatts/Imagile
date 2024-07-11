using Imagile.Data.Shared.Extensions;
using Imagile.Domain.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Imagile.Cli.Binders;
public class MasterDatabaseConnectionStringBinder : BinderBase<SqlConnectionStringBuilder>
{
    protected override SqlConnectionStringBuilder GetBoundValue(BindingContext bindingContext) =>
        Get(bindingContext.GetGlobalEnvironmentOption());

    public static SqlConnectionStringBuilder Get(ImagileEnvironment.Types environment)
    {
        var builder = new SqlConnectionStringBuilder { DataSource = environment.ToSqlServerName(), InitialCatalog = "master", };
        if (environment == ImagileEnvironment.Types.Local)
            builder.AddLocalAuthentication();
        return builder;
    }
}

public class SharedDbContextBinder : BinderBase<SharedDbContext>
{
    protected override SharedDbContext GetBoundValue(BindingContext bindingContext) =>
        Get(bindingContext.GetGlobalEnvironmentOption());
    public static SharedDbContext Get(ImagileEnvironment.Types environment)
    {
        var builder = new SqlConnectionStringBuilder { DataSource = environment.ToSqlServerName(), InitialCatalog = environment.ToSharedDatabaseName() };
        if (environment == ImagileEnvironment.Types.Local)
            builder.AddLocalAuthentication();
        var options = new DbContextOptionsBuilder<SharedDbContext>().UseSqlServer(builder.ToString()).Options;
        return new SharedDbContext(options);
    }
}


