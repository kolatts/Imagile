using Imagile.Domain.Authorization;

namespace Imagile.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class IncludesSecurityGroupsAttribute(params SecurityGroup.Ids[] includes) : IncludesAttribute<SecurityGroup.Ids>(includes);