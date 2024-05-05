using System.ComponentModel.DataAnnotations;

namespace Imagile.Data.Company.Interfaces;

public interface ICompanyIdEntity
{
    [Required]
    public int? CompanyId { get; set; }
}