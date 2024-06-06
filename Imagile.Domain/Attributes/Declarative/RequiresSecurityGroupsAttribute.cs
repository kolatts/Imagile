using Imagile.Domain.Authorization;

namespace Imagile.Domain.Attributes.Declarative;

[AttributeUsage(AttributeTargets.Field)]
public class RequiresSecurityGroupsAttribute(params SecurityGroup.Ids[] includes) : RequiresAttribute<SecurityGroup.Ids>(includes);