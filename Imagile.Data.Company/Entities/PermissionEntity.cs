using Imagile.Data.Company.Interfaces;
using Imagile.Domain.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.Data.Company.Entities;
[Table("Permissions")]
public class PermissionEntity : IReferenceEntity<PermissionEntity> //This is appended wih Entity because there is a domain version.
{
    [Key]
    public Permission.Ids PermissionId { get; set; }

    public Feature.Ids FeatureId { get; set; }

    [StringLength(1000)]
    public string Name { get; set; }

    public virtual FeatureEntity Feature { get; set; }
    
    public ICollection<SecurityGroupPermission>? SecurityGroupPermissions { get; set; }
    
    public static List<PermissionEntity> GetSeedData()
    {
        return Enum.GetValues<Permission.Ids>().Select(x => new PermissionEntity { PermissionId = x }).ToList();
    }
}
[Table("SecurityGroups")]
public class SecurityGroupEntity : IReferenceEntity<SecurityGroupEntity>
{

    public static List<SecurityGroupEntity> GetSeedData() => throw new NotImplementedException();
}
public class SecurityGroupPermission
{
    public SecurityGroup.Ids SecurityGroupId { get; set; }
    public Permission.Ids PermissionId { get; set; }
}
[Table("Features")]
public class FeatureEntity : IReferenceEntity<FeatureEntity>
{
    [Key]
    public Feature.Ids FeatureId { get; set; }

    public virtual ICollection<PermissionEntity> Permissions { get; set; }
    public static List<FeatureEntity> GetSeedData() => throw new NotImplementedException();
}