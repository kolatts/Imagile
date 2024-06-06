using Imagile.Domain.Authorization;

namespace Imagile.Domain.Attributes.Declarative;

[AttributeUsage(AttributeTargets.Field)]
public class IncludesFeaturesAttribute(params Feature.Ids[] includes) : IncludesAttribute<Feature.Ids>(includes);