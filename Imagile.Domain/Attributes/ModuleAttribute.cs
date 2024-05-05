using Imagile.Domain.Authorization;

namespace Imagile.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class ModuleAttribute(Module.Ids moduleId) : Attribute
{
    public Module.Ids ModuleId { get; set; } = moduleId;
}