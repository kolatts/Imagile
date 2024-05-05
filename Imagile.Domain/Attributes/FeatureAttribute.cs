using Imagile.Domain.Authorization;
namespace Imagile.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class FeatureAttribute(Feature.Ids feature) : Attribute
{
    public Feature.Ids FeatureId { get; set; } = feature;
}