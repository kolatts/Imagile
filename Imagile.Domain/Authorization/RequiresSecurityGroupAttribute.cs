using Imagile.Domain.Attributes.Declarative;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.Domain.Authorization;
[AttributeUsage(AttributeTargets.Field)]
public class RequiresSecurityGroupsAttribute(params SecurityGroup.Ids[] includes) : RequiresAttribute<SecurityGroup.Ids>(includes);


[AttributeUsage(AttributeTargets.Field)]
public class RequiresFeaturesAttribute(params Feature.Ids[] required) : RequiresAttribute<Feature.Ids>(required);

[AttributeUsage(AttributeTargets.Field)]
public class IncludesPermissionsAttribute(params Permission.Ids[] includes) : IncludesAttribute<Permission.Ids>(includes);