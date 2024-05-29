using Imagile.Data.Shared.Extensions;
using Imagile.Domain;
using Microsoft.Data.SqlClient;


namespace Imagile.Cli.Binders;
public class MasterDatabaseConnectionStringBinder : BinderBase<SqlConnectionStringBuilder>
{
    protected override SqlConnectionStringBuilder GetBoundValue(BindingContext bindingContext) => throw new NotImplementedException();

    public static SqlConnectionStringBuilder Get(ImagileEnvironment.Types environment)
    {
        var builder = new SqlConnectionStringBuilder { DataSource = environment.ToSqlServerName(), InitialCatalog = "master", };
        if (environment == ImagileEnvironment.Types.Local)
            builder.AddLocalAuthentication();
        return builder;
    }
}
