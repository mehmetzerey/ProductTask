namespace App.Domain.Models;

public class ApplicationRole : IdentityRole<int>
{
	public ApplicationRole()
	{

	}
	public ApplicationRole(string role):base(roleName:role)
	{
		
	}
}
