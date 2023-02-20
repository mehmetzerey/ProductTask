namespace App.Application.ModelView.UserView;

public class CreateUserViewModel
{
    [Required(ErrorMessage = "Ad zorunlu")]
    [DisplayName("Ad")]
    [StringLength(30)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Soyad zorunlu")]
    [DisplayName("Soyad")]
    [StringLength(30)]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Email adresi zorunlu")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Şifre zorunlu")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Girmiş olduğunuz parola birbiri ile eşleşmiyor.")]
    public string ConfirmPassword { get; set; }

    [DisplayName("Rol")]
    public int Role { get; set; } 
}
