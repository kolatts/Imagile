namespace Imagile.Domain.Attributes.Declarative;

[AttributeUsage(AttributeTargets.Field)]
public class RequiresAttribute<TEnum>(params TEnum[] required) : Attribute
{
    public TEnum[] Required { get; } = required;
    public bool RequireAll { get; } = true;
}