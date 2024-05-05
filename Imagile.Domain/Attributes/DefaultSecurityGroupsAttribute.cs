using Imagile.Domain.Authorization;

namespace Imagile.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class DefaultSecurityGroupsAttribute(params SecurityGroup.Ids[] addToSecurityGroups) : Attribute
{
    public List<SecurityGroup.Ids> SecurityGroupTypes { get; set; } = addToSecurityGroups.ToList();
}