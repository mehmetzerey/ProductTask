namespace App.Domain.Models;

public class ApplicationUserTokens : IdentityUserToken<int>
{
    public DateTime ExpireDate { get; set; }

}
