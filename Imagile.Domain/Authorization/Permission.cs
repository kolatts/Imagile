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
        ViewEmployees,
        EditEmployees,
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
        EmployeeManagement = 1,
    }
}

public static class Module
{
    public enum Ids
    {
        ImagileAdministration = -1,
        Core = 1,
    }
}