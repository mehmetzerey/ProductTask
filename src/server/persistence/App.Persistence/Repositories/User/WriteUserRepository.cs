namespace App.Persistence.Repositories.User;

public class WriteUserRepository : WriteRepository<ApplicationUser>, IWriteUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    public WriteUserRepository(AppIdentityDbContext appIdentity, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) : base(appIdentity)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<ServiceResponse> AddUserAsync(ApplicationUser user, string password, RoleEnum userRole)
    {
        var responseViewModel = new ServiceResponse();
        var existsUser = await _userManager.FindByNameAsync(user.Email);
        if (existsUser == null)
        {
            var result = await _userManager.CreateAsync(user, password.Trim());
            if (result.Succeeded)
            {
                bool roleExists = await _roleManager.RoleExistsAsync(userRole.Name);

                if (!roleExists)
                {
                    ApplicationRole role = new ApplicationRole(userRole.Name);
                    role.NormalizedName = userRole.Name;

                    _roleManager.CreateAsync(role).Wait();
                }

                //Kullanıcıya ilgili rol ataması yapılır.
                _userManager.AddToRoleAsync(user, userRole.Name).Wait();

                responseViewModel.IsSuccess = true;
                responseViewModel.Message = "Kullanıcı başarılı şekilde oluşturuldu.";
            }
            else
            {
                responseViewModel.IsSuccess = false;
                responseViewModel.Message = string.Format("Kullanıcı oluşturulurken bir hata oluştu: {0}", string.Join(", ", result.Errors.Select(x => x.Description)));
            }
        }

        return responseViewModel;
    }

    public async Task<ServiceResponse> DeleteUserAsync(ApplicationUser user)
    {
        var responseViewModel = new ServiceResponse();

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            responseViewModel.IsSuccess = true;
            responseViewModel.Message = "Kullanıcı başarılı şekilde silindi.";
        }
        else
        {
            responseViewModel.IsSuccess = false;
            responseViewModel.Message = "Kullanıcı bilgileri silinirken hata oldu.";
        }

        return responseViewModel;
    }

    public async Task<ServiceResponse> UpdateUserAsync(ApplicationUser user)
    {
        var responseViewModel = new ServiceResponse();

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            responseViewModel.IsSuccess = true;
            responseViewModel.Message = "Kullanıcı bilgileri başarılı şekilde güncellendi.";
        }
        else
        {
            responseViewModel.IsSuccess = false;
            responseViewModel.Message = "Kullanıcı bilgileri güncellenirken hata oldu.";
        }

        return responseViewModel;
    }
}

