using Imagile.Domain.Authorization;

namespace Imagile.Domain.Attributes;
[AttributeUsage(AttributeTargets.Field)]
public class RequiresAttribute<TEnum>(params TEnum[] required) : Attribute
{
    public TEnum[] Required { get; } = required;
    public bool RequireAll { get; } = true;
}

[AttributeUsage(AttributeTargets.Field)]
public class RequiresFeaturesAttribute( params Feature.Ids[] required) : RequiresAttribute<Feature.Ids>(required);