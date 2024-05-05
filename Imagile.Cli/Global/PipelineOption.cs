/// <summary>
/// Specifies the global pipeline option, which can be used by binders.
/// </summary>
public static class PipelineOption
{
    private static Option<bool> Initialize()
    {
        var option = new Option<bool>("--pipeline", () => false, "Specifies whether this is being run from a pipeline. This MUST be used by pipelines to prevent interactive authentication");
        option.AddAlias("-p");
        return option;
    }

    public static Option<bool> Value { get; } = Initialize();
    public static bool GetGlobalPipelineOption(this BindingContext bindingContext)
    {
        return bindingContext.ParseResult.GetValueForOption(Value);
    }
}