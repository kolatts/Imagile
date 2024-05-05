using Imagile.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace Imagile.Data.Shared.Entities;

public class CompanyConnection
{
    [Key]
    public int CompanyId { get; set; }

    [Required]
    public Guid? CompanyUnique { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    public CompanyStatusTypes StatusType { get; set; } = CompanyStatusTypes.Active;

    public bool IsTestSeedData { get; set; }
    public bool IsRealCustomer { get; set; }
    public int DatabaseShardId { get; set; }
    public virtual DatabaseShard DatabaseShard { get; set; } = null!;

    public virtual ICollection<LoginAccount> LoginAccounts { get; set; } = [];
}