namespace App.Application.ModelView;

public class ResetPasswordRequestModel
{
    [Display(Name = "E-Posta Adresiniz")]
    [Required(ErrorMessage = "Lütfen e-posta adresinizi boş geçmeyiniz.")]
    [EmailAddress(ErrorMessage = "Lütfen uygun formatta e-posta giriniz.")]
    public string Email { get; set; } = string.Empty;
}
