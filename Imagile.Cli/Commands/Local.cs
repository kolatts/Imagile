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
            await ImagileEnvironment.Types.Local.DeleteDatabases(progress);
        });
    }

  
}
