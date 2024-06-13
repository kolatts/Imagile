using Imagile.Cli.Binders;
using Imagile.Domain.Hosting;
using Microsoft.Data.SqlClient;

internal static class Database
{
    public static void AddDatabase(this RootCommand rootCommand)
    {
        var command = new Command("db", "Interacts with database for CI/CD and support functions");

    }

    public static async Task DeleteDatabases(this ImagileEnvironment.Types environment, ProgressContext progress)
    {
        var master = MasterDatabaseConnectionStringBinder.Get(environment);
        await using var connection = new SqlConnection(master.ConnectionString);
        await connection.OpenAsync();
        await progress.AddAndExecuteTask("Delete Company Database 1", async () => await DeleteDatabase(connection, environment.ToCompanyDatabaseName(1)));
        await progress.AddAndExecuteTask("Delete Company Database 2", async () => await DeleteDatabase(connection, environment.ToCompanyDatabaseName(2)));
        await progress.AddAndExecuteTask("Delete Shared Database", async () => await DeleteDatabase(connection, environment.ToSharedDatabaseName()));
        await connection.CloseAsync();
    }
    private static async Task DeleteDatabase(SqlConnection connection, string databaseName)
    {
        await using var checkCommand = connection.CreateCommand();
        checkCommand.CommandText = $"SELECT DB_ID('{databaseName}')";
        var dbId = await checkCommand.ExecuteScalarAsync();

        if (dbId != DBNull.Value)
        {
            await using var prepCommand = connection.CreateCommand();
            prepCommand.CommandText = $"ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
            await prepCommand.ExecuteNonQueryAsync();
            await using var command = connection.CreateCommand();
            command.CommandText = $"DROP DATABASE [{databaseName}]";
            await command.ExecuteNonQueryAsync();
        }
    }
}