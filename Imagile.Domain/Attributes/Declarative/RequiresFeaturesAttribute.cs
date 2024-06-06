using Imagile.Domain.Authorization;

namespace Imagile.Domain.Attributes.Declarative;

[AttributeUsage(AttributeTargets.Field)]
public class RequiresFeaturesAttribute(params Feature.Ids[] required) : RequiresAttribute<Feature.Ids>(required);