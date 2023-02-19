namespace App.Domain.Models;

public class ApplicationUser : IdentityUser<int>
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
}
