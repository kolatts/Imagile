using Imagile.Domain.Authorization;

namespace Imagile.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class IncludesPermissionsAttribute(params Permission.Ids[] includes) : IncludesAttribute<Permission.Ids>(includes);