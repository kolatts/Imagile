using System.ComponentModel.DataAnnotations;

public class LoginAccount
{
    [Required]
    public Guid? PersonUnique { get; set; }
    public CompanyConnection? Company { get; set; }

    public int CompanyId { get; set; }

    [StringLength(1000)]
    public string? SingleSignOnIdentifier { get; set; }
}