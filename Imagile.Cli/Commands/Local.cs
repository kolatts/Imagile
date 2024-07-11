using Imagile.Cli.Binders;
using Imagile.Domain.Hosting;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.Cli.Commands;
public static class Local
{
    public static RootCommand AddLocal(this RootCommand rootCommand)
    {
        var command = new Command("local", "Interacts with local development environment");
        var setupCommand = new Command("setup", "Sets up the local development environment.");
        command.Add(setupCommand);
        rootCommand.AddCommand(command);

        return rootCommand;
    }

    private static async Task SetupInternal()
    {
        await AnsiConsole.Progress()
            .SimpleColumns()
            .StartAsync(async progress =>
        {
            await DeleteDatabases(progress);
        });
    }

    public static async Task DeleteDatabases( ProgressContext progress)
    {
        var master = MasterDatabaseConnectionStringBinder.Get(ImagileEnvironment.Types.Local);
        await using var connection = new SqlConnection(master.ConnectionString);
        await connection.OpenAsync();
        await progress.AddAndExecuteTask("Delete Company Database 1", async () => await DeleteDatabase(connection, ImagileEnvironment.Types.Local.ToCompanyDatabaseName(1)));
        await progress.AddAndExecuteTask("Delete Company Database 2", async () => await DeleteDatabase(connection, ImagileEnvironment.Types.Local.ToCompanyDatabaseName(2)));
        await progress.AddAndExecuteTask("Delete Shared Database", async () => await DeleteDatabase(connection, ImagileEnvironment.Types.Local.ToSharedDatabaseName()));
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

    public static async Task CreateSharedDatabase(this ImagileEnvironment.Types environment, ProgressContext progress)
    {
        var master = MasterDatabaseConnectionStringBinder.Get(environment);
        await using var connection = new SqlConnection(master.ConnectionString);
        await connection.OpenAsync();
        await progress.AddAndExecuteTask("Create Shared Database", async () => await CreateDatabase(connection, environment.ToSharedDatabaseName()));
        await connection.CloseAsync();
    }
}
