namespace App.Application.Repositories.User;

public interface IReadUserRepository : IReadRepository<ApplicationUser>
{
    Task<ApplicationUser> FindByNameAsync(string userName);
    Task<ApplicationUser> FindByIdAsync(int userId);
    Task<ApplicationUser> FindByEmail(string email);
    List<ApplicationUser> GetAll();
    Task<List<ApplicationUser>> GetUserByRole(int roleId);

}
