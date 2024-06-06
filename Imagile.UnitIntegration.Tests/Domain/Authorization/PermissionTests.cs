using FluentAssertions;
using Imagile.Domain.Authorization;
using Imagile.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.UnitIntegration.Tests.Domain.Authorization;
public class PermissionTests
{
    [Test]
    public void GetIncludedPermissions_EditUsers()
    {
        var permission = Permission.Ids.EditUsers;

        var included = permission.GetIncluded().ToList();

        included.Should().HaveCount(1);
        included.Should().Contain(Permission.Ids.ViewUsers);
    }
}
