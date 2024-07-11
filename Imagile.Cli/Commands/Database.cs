using Imagile.Cli.Binders;
using Imagile.Domain.Hosting;
using Microsoft.Data.SqlClient;

internal static class Database
{
    public static void AddDatabase(this RootCommand rootCommand)
    {
        var command = new Command("db", "Interacts with database for CI/CD and support functions");

    }
}