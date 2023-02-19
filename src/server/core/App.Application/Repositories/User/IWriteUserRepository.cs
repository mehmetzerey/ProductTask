namespace App.Application.Repositories.User;

public interface IWriteUserRepository : IWriteRepository<ApplicationUser>
{
    Task<ServiceResponse> AddUserAsync(ApplicationUser user, string password);
}
