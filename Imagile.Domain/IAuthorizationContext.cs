

namespace Imagile.Domain;
/// <summary>
/// This interface is used to provide the authorization context for the current user.
/// </summary>
public interface IAuthorizationContext
{
    /// <summary>
    /// The company attempting to be accessed. Customer users can only access their own company.
    /// </summary>
    public int? CompanyId { get; }
    /// <summary>
    /// The person ID attempting to access the system.
    /// </summary>
    public int? PersonId { get; }
    /// <summary>
    /// This is the unique identifier for the person, and associated with the person's login.
    /// </summary>
    public Guid? PersonUniqueId { get; }
    /// <summary>
    /// This is the company that should be used for checking if the user has the correct permissions.
    /// This will be the same as the CompanyId for customer users.
    /// </summary>
    public int? AuthorizedByCompanyId { get;  }
    /// <summary>
    /// Determines whether this user is an Imagile administrator, which generally provides looser authorization controls.
    /// </summary>
    public bool IsImagileAdministrator { get; }
}
/// <summary>
/// This authorization context is used when the user is not logged in, by processes that are not user initiated.
/// Therefore, the context can be created based on queue messages, developer is more in control of assigning the values.
/// <inheritdoc cref="IAuthorizationContext"/>
/// </summary>
public class CloudAuthorizationContext : IAuthorizationContext
{
    public int? CompanyId { get; set; }
    public int? PersonId { get; set; }
    public Guid? PersonUniqueId { get; set; }
    public int? AuthorizedByCompanyId { get; set; }
    public bool IsImagileAdministrator { get; set; }
}