using System.ComponentModel.DataAnnotations;

namespace Imagile.Data.Shared;

/// <summary>
/// All settings for the API/Hosted services.
/// </summary>
public class ImagileHostingSettings
{
    public string? ManagedIdentityClientId { get; set; }
    [Required]
    public string? SharedDatabaseConnectionString { get; set; }
}