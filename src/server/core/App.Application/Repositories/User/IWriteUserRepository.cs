using App.Domain.Enums;

namespace App.Application.Repositories.User;

public interface IWriteUserRepository : IWriteRepository<ApplicationUser>
{
    Task<ServiceResponse> AddUserAsync(ApplicationUser user, string password, RoleEnum userRole);
    Task<ServiceResponse> UpdateUserAsync(ApplicationUser user);
    Task<ServiceResponse> DeleteUserAsync(ApplicationUser user);
}
