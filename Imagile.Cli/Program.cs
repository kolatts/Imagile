﻿using System.CommandLine.Builder;
using System.CommandLine.Help;
using System.CommandLine.Parsing;

var rootCommand = new RootCommand("Imagile CLI") { Name = "imagile" };
rootCommand.AddGlobalOption(EnvironmentOption.Value);
rootCommand.AddGlobalOption(PipelineOption.Value);
var parser = new CommandLineBuilder(rootCommand)
    .UseDefaults()
    .UseHelp(help =>
    {
        help.HelpBuilder.CustomizeLayout((_ => HelpBuilder.Default
            .GetLayout()
            .Skip(1)
            .Prepend(_ =>
            {
                AnsiConsole.Write(new FigletText(rootCommand.Description!).Color(Display.BrandColor));
            })
            ));
    })
    .AddMiddleware(async (context, next) =>
    {
        if (!context.ParseResult.GetValueForOption(PipelineOption.Value))
        {
#if !DEBUG
            try
            {
                // CliVersion.Check();
            }
            catch
            {
                //Ignore version check if access not setup correctly.
            }
#endif
        }
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            Display.LogError(ex.ToString());
            context.ExitCode = 1;
        }
    }).Build();
return await parser.InvokeAsync(args);