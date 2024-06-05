using Imagile.Domain.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.Domain.Attributes;
[AttributeUsage(AttributeTargets.Field)]
public class IncludesAttribute<TEnum>(params TEnum[] includes) : Attribute
{
    public TEnum[] Includes { get; protected set; } = includes;
}

[AttributeUsage(AttributeTargets.Field)]
public class IncludesFeaturesAttribute(params Feature.Ids[] includes) : IncludesAttribute<Feature.Ids>(includes);

[AttributeUsage(AttributeTargets.Field)]
public class IncludesPermissionsAttribute(params Permission.Ids[] includes) : IncludesAttribute<Permission.Ids>(includes);

[AttributeUsage(AttributeTargets.Field)]
public class IncludesSecurityGroupsAttribute(params SecurityGroup.Ids[] includes) : IncludesAttribute<SecurityGroup.Ids>(includes);

[AttributeUsage(AttributeTargets.Field)]
public class IncludesAllSecurityGroupsAttribute : IncludesSecurityGroupsAttribute
{
    public IncludesAllSecurityGroupsAttribute()
    {
        Includes = [.. Enum.GetValues<SecurityGroup.Ids>()];
    }
}