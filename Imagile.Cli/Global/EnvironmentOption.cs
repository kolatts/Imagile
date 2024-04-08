using Imagile.Domain;

/// <summary>
/// Specifies the global environment option, which can be used by binders.
/// </summary>
public static class EnvironmentOption
{
    private static Option<ImagileEnvironment.Types> Initialize()
    {
        var option = new Option<ImagileEnvironment.Types>("--env", () => ImagileEnvironment.Types.Local, "The environment to target");
        option.AddAlias("-e");
        return option;
    }

    public static Option<ImagileEnvironment.Types> Value { get; } = Initialize();

    public static ImagileEnvironment.Types GetGlobalEnvironmentOption(this BindingContext bindingContext)
    {
        return bindingContext.ParseResult.GetValueForOption(EnvironmentOption.Value);
    }
}