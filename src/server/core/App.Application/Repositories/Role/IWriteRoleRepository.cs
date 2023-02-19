namespace App.Application.Repositories.Role;

public interface IWriteRoleRepository : IWriteRepository<ApplicationRole>
{
    Task<ServiceResponse> AddRoleAsync(string role);
}
