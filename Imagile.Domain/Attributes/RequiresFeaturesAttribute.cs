using Imagile.Domain.Authorization;

namespace Imagile.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class RequiresFeaturesAttribute( params Feature.Ids[] required) : RequiresAttribute<Feature.Ids>(required);