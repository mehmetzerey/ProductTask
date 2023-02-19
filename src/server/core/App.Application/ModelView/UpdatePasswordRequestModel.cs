namespace App.Application.ModelView;

public class UpdatePasswordRequestModel
{
    public string Password { get; set; } = string.Empty;
    public string userId { get; set; } = string.Empty;
    public string token { get; set; } = string.Empty;
}
