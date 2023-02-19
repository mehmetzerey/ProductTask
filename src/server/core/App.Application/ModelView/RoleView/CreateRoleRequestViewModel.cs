namespace App.Application.ModelView.RoleView;

public class CreateRoleRequestViewModel
{
    [Required]
    public string Role { get; set; } = string.Empty;
}
