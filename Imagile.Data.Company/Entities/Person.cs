using Imagile.Data.Company.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Imagile.Data.Company.Entities;

public class Person : ICompanyIdEntity
{
    [Required]
    public int? CompanyId { get; set; }
    public int PersonId { get; set; }
    [Required]
    [StringLength(255)]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(255)]
    public string? LastName { get; set; }

    [StringLength(255)]
    public string MiddleName { get; set; } = string.Empty;

    [StringLength(255)]
    public string SuffixName { get; set; } = string.Empty;

    [StringLength(255)]
    public string? PreferredName { get; set; }

    [Required]
    [StringLength(255)]
    [EmailAddress]
    public string? Email { get; set; }


}