using Imagile.Domain.Attributes.Declarative;

namespace Imagile.Domain.Authorization;

public static class Permission
{
    public enum Ids
    {
        [RequiresSecurityGroups(SecurityGroup.Ids.ImagileViewOnly)]
        ViewUsers,
        [IncludesPermissions(ViewUsers)]
        [RequiresFeatures(Feature.Ids.UserManagement)]
        [RequiresSecurityGroups(SecurityGroup.Ids.CompanyAdministrator, SecurityGroup.Ids.ImagileAdministrator)]
        EditUsers
    }
}

public static class SecurityGroup
{
    public enum Ids
    {
        ImagileAdministrator = -1000,
        ImagileViewOnly = -1,
        CompanyAdministrator = 1,
    }
}

public static class Feature
{
    public enum Ids
    {
        UserManagement,
        [Includes<Ids>(UserManagement)]
        CompanyManagement
    }
}

