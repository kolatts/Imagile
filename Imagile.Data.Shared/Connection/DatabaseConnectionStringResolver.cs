using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Imagile.Data.Shared.Connection;

public interface IDatabaseConnectionStringResolver
{
    SqlConnectionStringBuilder Resolve(string dataSource, string initialCatalog);
    SqlConnectionStringBuilder ResolveSharedConnectionString();
}

public class DatabaseConnectionStringResolver(IOptions<ImagileHostingSettings> options) : IDatabaseConnectionStringResolver
{

    public SqlConnectionStringBuilder Resolve(string dataSource, string initialCatalog)
    {
        var builder = new SqlConnectionStringBuilder
        {
            DataSource = dataSource,
            InitialCatalog = initialCatalog
        };
        if (builder.DataSource.Contains("localhost", StringComparison.InvariantCultureIgnoreCase))
        {
            builder.TrustServerCertificate = true;
            builder.UserID = "sa";
            builder.Password = "P@ssw0rd!";
        }
        else
        {
            if (options.Value.ManagedIdentityClientId != null)
            {
                builder.Authentication = SqlAuthenticationMethod.ActiveDirectoryManagedIdentity;
                builder.UserID = options.Value.ManagedIdentityClientId;
            }
            else
            {
                builder.Authentication = SqlAuthenticationMethod.ActiveDirectoryInteractive;
            }

            builder.Encrypt = true;
        }
        return builder;
    }

    public SqlConnectionStringBuilder ResolveSharedConnectionString()
    {
        var builder = new SqlConnectionStringBuilder(options.Value.SharedDatabaseConnectionString);
        return Resolve(builder.DataSource, builder.InitialCatalog);
    }


}