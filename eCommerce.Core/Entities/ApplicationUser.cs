namespace eCommerce.Core.Entities;

/// <summary>
/// Define the ApplicationUser class which acts as a model class for user information 
/// in the application.
/// </summary>

public class ApplicationUser
{
    public Guid UserId { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PersonName { get; set; }
    public string? Gender { get; set; }
}

