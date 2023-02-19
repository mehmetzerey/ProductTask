using App.Application.Repositories.Role;
using App.Application.Repositories;
using App.Domain.Models;
using App.Domain;
using Microsoft.AspNetCore.Identity;

namespace App.Persistence.Repositories.Role;

public class WriteRoleRepository : WriteRepository<ApplicationRole>, IWriteRoleRepository
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    public WriteRoleRepository(AppIdentityDbContext appIdentity, RoleManager<ApplicationRole> roleManager) : base(appIdentity)
    {
        _roleManager = roleManager;
    }

    public async Task<ServiceResponse> AddRoleAsync(string role)
    {
        role = role.ToLower();
        var response = new ServiceResponse();
        bool roleExists = await _roleManager.RoleExistsAsync(role);

        if (!roleExists)
        {
            var createRole = new ApplicationRole(role);
            createRole.NormalizedName = role.ToUpper();

            _roleManager.CreateAsync(createRole).Wait();
            response.IsSuccess = true;
            response.Message = $"Rol oluşturuldu {role}";
        }
        else
        {
            response.IsSuccess = false;
            response.Message = $"{role} zaten kayıtlı";
        }
        return response;
    }
}

