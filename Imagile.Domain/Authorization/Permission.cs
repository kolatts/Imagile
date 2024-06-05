using Imagile.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.Domain.Authorization;

public static class Permission
{
    public enum Ids
    {
        [IncludesSecurityGroups(SecurityGroup.Ids.ImagileViewOnly)]
        ViewUsers,
        [IncludesPermissions(EditUsers)]
        [RequiresFeatures(Feature.Ids.UserManagement)]
        [IncludesSecurityGroups(SecurityGroup.Ids.CompanyAdministrator, SecurityGroup.Ids.ImagileAdministrator)]
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
      
        UserManagement
    }
}

